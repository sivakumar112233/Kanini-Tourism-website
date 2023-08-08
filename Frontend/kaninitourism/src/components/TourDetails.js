import React, { useState, useEffect } from 'react';
import { Carousel } from 'react-responsive-carousel';
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { useParams,Link } from 'react-router-dom';
import './TourDetails.css';
import Navbar from './Navbar';
import Footer from './Footer';
function TourDetails() {
  const { tourId } = useParams();
  const [tourDetails, setTourDetails] = useState({});
  const [tourImages, setTourImages] = useState([]);

  useEffect(() => {
    fetch(`http://localhost:5299/api/Tour/GettingATourByTourId?tourId=${tourId}`)
      .then(response => response.json())
      .then(data => setTourDetails(data))
      .catch(error => console.error(error));

    fetch(`http://localhost:5287/api/TourImages/GettingAllImagesByTourId?tourId=${tourId}`)
      .then(response => response.json())
      .then(images => setTourImages(images))
      .catch(error => console.error(error));
  }, [tourId]);

  if (!tourDetails.tourName) {
    return <p>Loading...</p>;
  }

  return (
    <div className="tour-details-container">
      <div className='navbar'>
        <Navbar/>
      </div>
      {tourDetails && (
        <div>
          <h2>{tourDetails.tourName}</h2>
          <Carousel showThumbs={false}>
            {tourImages.map((images, index) => (
              <div key={index}>
                <img src={images.imagePaths} alt={` ${index}`} />
              </div>
            ))}
          </Carousel>
          <div className="details">
            <p className="detail">Price: ${tourDetails.tourPrice}</p>
            <p className="detail">Location: {tourDetails.tourLocationState}, {tourDetails.tourLocationCountry}</p>
            <p className="detail">Type: {tourDetails.tourName}</p>
          </div>
          <table className="inclusions-table">
            <thead>
              <tr>
                <th>Inclusions</th>
                <th>Details</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>Accommodation</td>
                <td>{tourDetails.inclusion.accommodation}</td> 
              </tr>
              <tr>
                <td>Meals</td>
                <td>{tourDetails.inclusion.meals}</td>
              </tr>
              <tr>
                <td>Transfer</td>
                <td>{tourDetails.inclusion.transfer}</td>
              </tr>
              <tr>
                <td>Total Nights</td>
                <td>{tourDetails.inclusion.totalNights}</td>
              </tr>
              <tr>
                <td>Total Days</td>
                <td>{tourDetails.inclusion.totalDays}</td>
              </tr>
              {/* Add more inclusions */}
            </tbody>
          </table>
          <div className="day-descriptions">
            {tourDetails.inclusion.totalDaysDescriptions.map((description, index) => (
              <div key={index} className="day-description">
                <div className={`day-circle ${index === 0 ? 'filled' : ''}`}>{index + 1}</div>
                <div className="day-card">
                  <p>{description.visitingPlaceName}</p>
                  <p>{description.dayDescription}</p>
                </div>
              </div> 
            ))}
            <div>
            <Link to={`/booking/${tourId}/${tourDetails.tourPrice}/${tourDetails.inclusion.totalNights}`}>
                <button>Book Now</button>
              </Link>
              </div>
          </div>
          
        </div>
      ) }
      <div className='footer'>
        <Footer/>
      </div>
    </div>
  );
}

export default TourDetails;
