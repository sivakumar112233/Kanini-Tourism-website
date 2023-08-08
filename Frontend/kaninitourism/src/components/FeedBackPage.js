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

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFeedback((prevFeedback) => ({
      ...prevFeedback,
      [name]: value
    }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

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
          <br />
          <br />
          <button type="submit">Submit Feedback</button>
        </form>
      </div>
    </div>
    <Footer/>
    </div>
  );
}

export default FeedBackPage;
