import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { BiHome, BiEnvelope, BiMessage, BiInfoCircle, BiLogOut } from 'react-icons/bi'; // Import the required icons

import './Navbar.css';

function Navbar() {
  const navigate = useNavigate();

  const handleLogout = () => {
    // Clear local storage and redirect to the landing page
    localStorage.clear();
    navigate('/');
  };

  return (
    <div className='travellerNavBar'>
      <ul>
        <li><Link to="/tourPackages"><BiHome size={30} className="icon" /> kaniniTourism</Link></li>
        <li><Link to="/contact"><BiEnvelope size={30} className="icon" /> Contact</Link></li>
        <li><Link to="/feedback"><BiMessage size={30} className="icon" /> Feedback</Link></li>
        <li><Link to="/about"><BiInfoCircle size={30} className="icon" /> About Us</Link></li>
        <li className="logouts" style={{"color":"white"}}onClick={handleLogout}><BiLogOut size={30} className="icon" /> Logout</li>
      </ul>
    </div>
  );
}

export default Navbar;
