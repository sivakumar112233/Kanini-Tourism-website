import React from 'react';
import { FaGoogle, FaYahoo, FaFacebook, FaCar, FaBus, FaHotel } from 'react-icons/fa';
import about from '../Images/about.jpeg';
import './AboutUs.css';
import Footer from './Footer';
import Navbar from './Navbar';

function AboutUs() {
  return (
    <div>
      <div>
        <Navbar/>
      </div>
      <div className='aboutUsContainer'>
        <div className='aboutUsImage'>
          <img src={about} alt="about" />
        </div>
        <div className='aboutUsDescription'>
          <h1>About Kanini Tourism</h1>
          <div>
                        ripadvisor, the world's largest travel platform*,
                        helps 463 million travelers each month** make every trip their best trip. 
                        Travelers across the globe use the Tripadvisor site and app to browse more than 859 
                        million reviews and opinions of 8.6 million accommodations, restaurants, experiences, airlines and cruises. Whether planning or on a trip, travelers turn to Tripadvisor to compare low prices on hotels, flights and cruises, 
                        book popular tours and attractions, as well as reserve tables at great restaurants. 
                        Tripadvisor,
                        the ultimate travel companion, is available in 49 markets and 28 languages.
                        The subsidiaries and affiliates of Tripadvisor, Inc. (NASDAQ:TRIP)
                        own and operate a portfolio of websites and businesses, including the 
                        following travel media brands:
                        www.bokun.io, www.cruisecritic.com, www.flipkey.com, www.thefork.com,                            www.helloreco.com, www.holidaylettings.co.uk, www.housetrip.com, www.jetsetter.com,  www.niumba.com, www.seatguru.com, www.singleplatform.com, www.vacationhomerentals.com, and www.viator.com.
                        *   Source: Jumpshot for Tripadvisor Sites, worldwide, November 201
                        ** Source: Tripadvisor internal log files, average monthly unique visitors, 
                        Q3 2019
                    </div>
        </div>
        <div className='aboutSectionOne'>
          <h1>Our Partners</h1>
          <ul>
            <li><FaGoogle /> Google</li>
            <li><FaYahoo /> Yahoo</li>
            <li><FaFacebook /> Facebook</li>
          </ul>
        </div>
        <div className='travelPartners'>
          <h1>Travel Partners</h1>
          <ul>
            <li><FaCar /> Ola</li>
            <li><FaBus /> Red Bus</li>
          </ul>
        </div>
        <div className='hotelPartners'>
          <h1>Hotel Partners</h1>
          <ul>
            <li><FaHotel /> Ola</li>
            <li><FaHotel /> Red Bus</li>
          </ul>
        </div>
      </div>
      <div>
        <Footer/>
      </div>
    </div>
  );
}

export default AboutUs;
