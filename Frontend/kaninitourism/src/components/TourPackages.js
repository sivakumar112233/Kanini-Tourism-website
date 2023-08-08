// TourPackage.js
import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { FaUmbrellaBeach, FaLandmark, FaPaw, FaChurch, FaHiking } from 'react-icons/fa'; // Import icons
import './TourPackages.css';
import Footer from './Footer';
import {

  FaDollarSign,
  FaMapMarkerAlt,
  FaCalendar,
} from 'react-icons/fa';
import Navbar from './Navbar';

function TourPackages() {
  const [tourPackages, setTourPackages] = useState([]);
  const [selectedSpecialty, setSelectedSpecialty] = useState('beach');

  useEffect(() => {
    fetch(`http://localhost:5299/api/Tour/GettingAllTourNamesBySpeciality?tourSpecialty=${selectedSpecialty}`)
      .then(response => response.json())
      .then(data => {
        const toursWithInfoPromises = data.map(tour => {
          const tourDetailsPromise =  fetch(`http://localhost:5299/api/Tour/GettingATourByTourId?tourId=${tour.tourId}`)
            .then(response => response.json());
          const tourImagesPromise =fetch(`http://localhost:5287/api/TourImages/GettingAllImagesByTourId?tourId=${tour.tourId}`)
            .then(response => response.json()
            );

          return Promise.all([tourDetailsPromise, tourImagesPromise])
            .then(([details, images]) => {
              const totalNights = details?.inclusion?.totalNights || 0;
              return {
                tourId: tour.tourId,
                tourName: tour.tourName,
                tourPrice: details.tourPrice || 0,
                totalNights: totalNights,
                images: images,
              };
            });
        });

        Promise.all(toursWithInfoPromises)
          .then(toursWithInfo => setTourPackages(toursWithInfo))
          .catch(error => console.error(error));
      })
      .catch(error => console.error(error));
  }, [selectedSpecialty]);

  return (


  
    <div className='tourpackageContainer'>
      <div className='navBarSection'>
       <Navbar/>
      </div>
      <div className='navSection'>
        <button onClick={() => setSelectedSpecialty('beach')} className={selectedSpecialty === 'beach' ? 'active' : ''}>
          <FaUmbrellaBeach /> Beach
        </button>
        <button onClick={() => setSelectedSpecialty('heritage')} className={selectedSpecialty === 'heritage' ? 'active' : ''}>
          <FaLandmark /> Heritage
        </button>
        <button onClick={() => setSelectedSpecialty('wildlife')} className={selectedSpecialty === 'wildlife' ? 'active' : ''}>
          <FaPaw /> Wildlife
        </button>
        <button onClick={() => setSelectedSpecialty('Pilgrimage')} className={selectedSpecialty === 'Pilgrimage' ? 'active' : ''}>
          <FaChurch /> Pilgrimage
        </button>
        <button onClick={() => setSelectedSpecialty('Adventure')} className={selectedSpecialty === 'Adventure' ? 'active' : ''}>
          <FaHiking /> Adventure
        </button>
      </div>
      <div className='PackageSection'>
        <h2 className='selectedPackageHead'>{selectedSpecialty === 'beach' ? 'Popular Beach Packages' : selectedSpecialty === 'heritage' ? 'Heritage Packages' : selectedSpecialty === 'wildlife' ? 'Wildlife Packages' :selectedSpecialty === 'Pilgrimage' ? 'Pilgrimage Packages':selectedSpecialty === 'heritage' ? 'Heritage Packages' : selectedSpecialty === 'wildlife' ? 'Wildlife Packages' :selectedSpecialty === 'Adventure' ? ' AdventurePackages' :''}</h2>
        <div className='tourCardsContainer'>
        {tourPackages.map(tour => (
          <div key={tour.tourId} className="tourCard">
            <Link to={`/tour-details/${tour.tourId}`}>
              <img src={tour.images[0]?.imagePaths} alt={tour.tourName} />
            </Link>
            <div className="tourDetails">
              <div className="tourIcon">
                <FaUmbrellaBeach className="icon" />
                <span>{tour.tourName}</span>
              </div>
              <div className="tourInfo">
                <div className="tourPrice">
                  <FaDollarSign className="icon orange" />
                  <span>${tour.tourPrice}</span>
                </div>
                <div className="tourLocation">
                  <FaMapMarkerAlt className="icon orange" />
                  <span>{tour.location}</span>
                </div>
                <div className="tourDuration">
                  <FaCalendar className="icon orange" />
                  <span>
                    {tour.totalDays} days/{tour.totalNights} nights
                  </span>
                </div>
              </div>
              <Link to={`/tour-details/${tour.tourId}`} className="read-more">
                Read More
              </Link>
            </div>
          </div>
        ))}
        </div>
      </div>
      <Footer className="tourFooter" />
    </div>
      

  );
}

export default TourPackages;
