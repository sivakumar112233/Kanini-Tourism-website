import React from 'react';
import { FaEnvelope, FaMapMarkerAlt, FaPhone } from 'react-icons/fa';
import contact from '../Images/contact.jpg';
import './ContactPage.css';
import Footer from './Footer';
import Navbar from './Navbar';

function ContactPage() {
    return (
        <div className='contact'>
            <Navbar/>
            <div className="contactPageContainer">
                <div className="contactImageSection">
                    <img src={contact} alt="contact" />
                </div>
                <div className="contactSection">
                    <h2>Contact Information</h2>
                    <p><FaPhone /> Mobile number: +91 333333</p>
                    <p><FaMapMarkerAlt /> Address: Chennai, Karapakkam, Tamil Nadu</p>
                    <p><FaEnvelope /> Email: KaniniTourism@gmail.com</p>
                    <input type="email" placeholder="Your Email" />
                    <button className="subscribe-button">Subscribe Us</button>
                </div>
            </div>
            <div className='footerContact'>
               <Footer/>
            </div>
        </div>
    );
}

export default ContactPage;
