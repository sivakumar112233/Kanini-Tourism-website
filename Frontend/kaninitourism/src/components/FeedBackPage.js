import React, { useState } from 'react';
import feedbacks from '../Images/feedback.jpg';
import './FeedBackPage.css';
import Navbar from './Navbar';
import Footer from './Footer';

function FeedBackPage() {
  const [feedback, setFeedback] = useState({
    feedbackId: 0,
    feedbackDescription: '',
    tourName: '',
    tourDescription: '',
    rating: null,
    userId: 0
  });

  const [errors, setErrors] = useState({});
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleInputChange = (event) => {
    const { name, value } = event.target;


    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: '',
    }));

    setFeedback((prevFeedback) => ({
      ...prevFeedback,
      [name]: value,
    }));
  };

  const validateForm = () => {
    let validationErrors = {};

    if (!feedback.tourName.trim()) {
      validationErrors.tourName = 'Tour Name is required';
    }

    if (!feedback.tourDescription.trim()) {
      validationErrors.tourDescription = 'Tour Description is required';
    }

    if (feedback.rating === null || feedback.rating < 1 || feedback.rating > 5) {
      validationErrors.rating = 'Rating must be between 1 and 5';
    }

    setErrors(validationErrors); 
    return Object.keys(validationErrors).length === 0;
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    setIsSubmitting(true);
  
    if (validateForm()) {
      try {
        const response = await fetch('http://localhost:5105/api/FeedBackAddingANewFeedBack', {
          method: 'POST',
          headers: {
            'Accept': 'text/plain',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(feedback)
        });
  
        if (response.ok) {
          console.log('Feedback submitted successfully');
        } else {
          console.log('Feedback submission failed');
        }
      } catch (error) {
        console.error('Error submitting feedback:', error);
      }
    } else {
      console.log('Form has validation errors. Cannot submit.');
    }
  
    setIsSubmitting(false);
  };
  
  return (
    <div>
      <Navbar/>
      <div className='feedBackHeader'>
        <div className='feedBackImage'>
          <img src={feedbacks} alt="logo" />
        </div>
        <div className='feedbackForm'>
          <h2>Feedback Form</h2>
          <form onSubmit={handleSubmit}>
            <label>
              Tour Name:
              <input
                type="text"
                name="tourName"
                value={feedback.tourName}
                onChange={handleInputChange}
              />
            </label>
            {errors.tourName && <span className="error">{errors.tourName}</span>}
            <br />
            <label>
              Tour Description:
            </label>
            <textarea
              name="tourDescription"
              value={feedback.tourDescription}
              onChange={handleInputChange}
              rows="5"
              style={{ width: '100%' }} 
            />
            {errors.tourDescription && <span className="error">{errors.tourDescription}</span>}
            <br />
            <label>
              Rating:
              <input
                type="number"
                name="rating"
                value={feedback.rating || ''}
                onChange={handleInputChange}
              />
            </label>
            {errors.rating && <span className="error">{errors.rating}</span>}
            <br />
            <br />
            <button type="submit" disabled={isSubmitting}>Submit Feedback</button>
          </form>
        </div>
      </div>
      <Footer/>
    </div>
  );
}

export default FeedBackPage;
