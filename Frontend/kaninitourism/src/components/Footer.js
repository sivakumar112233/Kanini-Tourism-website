
import React from 'react';
import { FaFacebook, FaInstagram, FaYoutube, FaTwitter } from 'react-icons/fa';
import './Footer.css';

function Footer() {
  return (
    <div className="footer">
      <div className="ourPartnerSection">
        
      </div>
      <div className="socialMedia">
        <FaFacebook className="social-icon"/>
        <FaInstagram className="social-icon" />
        <FaYoutube className="social-icon" />
        <FaTwitter className="social-icon" />
      </div>
      <div className="copyRightsSection">
        Copyright © 2019 All rights reserved.
      </div>
    </div>
  );
}

export default Footer;
