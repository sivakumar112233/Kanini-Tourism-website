
import React, { useState } from 'react';
import './LandingPage.css';
import Wild from '../Images/wild.jpeg';
import heritage from '../Images/heritage.jpeg';
import prilimage from '../Images/prilimage.jpg';
import adventure from '../Images/adventure.jpg';
import ag from '../Images/ag.jpeg';
import brand from'../Images/brand.png';
import 'bootstrap-icons/font/bootstrap-icons.css';
import { useNavigate } from 'react-router-dom';


import { BiUser, BiEnvelope, BiPhone, BiLock, BiMap, BiBuilding, BiUserPlus, BiCheckCircle } from 'react-icons/bi';

import Footer from './Footer';
function LandingPage() {
    const [isAgentModalOpen, setIsAgentModalOpen] = useState(false);
    const [isTravellerModalOpen, setIsTravellerModalOpen] = useState(false);
    const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
    const [agentForm, setAgentForm] = useState({
      username: '',
      email: '',
      agencyName: '',
      contactNumber: '',
      agentName: '',
      address: '',
      passwordClear: ''
    });
    const [travellerForm, setTravellerForm] = useState({
      username: '',
      email: '',
      phoneNumber: '',
      gender: '',
      passwordClear: ''
    });
    const [loginForm, setLoginForm] = useState({
      email: '',
      password: ''
    });

  const [agentFormErrors, setAgentFormErrors] = useState({
    username: '',
    email: '',
    agencyName: '',
    contactNumber: '',
    agentName: '',
    address: '',
    passwordClear: ''
  });

  const [travellerFormErrors, setTravellerFormErrors] = useState({
    username: '',
    email: '',
    phoneNumber: '',
    gender: '',
    passwordClear: ''
  });

  const [loginFormErrors, setLoginFormErrors] = useState({
    email: '',
    password: ''
  });


  
    const navigate = useNavigate();
  
    
    const closeAgentModal = () => setIsAgentModalOpen(false);
    
    const closeTravellerModal = () => setIsTravellerModalOpen(false);
   
    const closeLoginModal = () => setIsLoginModalOpen(false);
  
    const handleAgentChange = (event) => {
        const { name, value } = event.target;
        setAgentForm({
          ...agentForm,
          [name]: value
        });
      
     
        setAgentFormErrors({
          ...agentFormErrors,
          [name]: ''
        });
      };
      
      const handleTravellerChange = (event) => {
        const { name, value } = event.target;
        setTravellerForm({
          ...travellerForm,
          [name]: value
        });
      
    
        setTravellerFormErrors({
          ...travellerFormErrors,
          [name]: ''
        });
      };
      
      const handleLoginChange = (event) => {
        const { name, value } = event.target;
        setLoginForm({
          ...loginForm,
          [name]: value
        });
      
      
        setLoginFormErrors({
          ...loginFormErrors,
          [name]: ''
        });
      };

      
    const validateAgentForm = () => {
      const errors = {};
      if (!agentForm.username) {
        errors.username = 'Username is required';
      }
      if (!agentForm.email) {
        errors.email = 'Email is required';
      }
      if (!agentForm.agencyName) {
        errors.agencyName = 'Agency Name is required';
      }
      if (!agentForm.contactNumber) {
        errors.contactNumber = 'Contact Number is required';
      }
      if (!agentForm.agentName) {
        errors.agentName = 'Agent Name is required';
      }
      if (!agentForm.address) {
        errors.address = 'Address is required';
      }
      if (!agentForm.passwordClear) {
        errors.passwordClear = 'Password is required';
      }
      setAgentFormErrors(errors);
      return Object.keys(errors).length === 0;
    };
  
    const validateTravellerForm = () => {
      const errors = {};
      if (!travellerForm.username) {
        errors.username = 'Username is required';
      }
      if (!travellerForm.email) {
        errors.email = 'Email is required';
      }
      if (!travellerForm.phoneNumber) {
        errors.phoneNumber = 'Phone Number is required';
      }
      if (!travellerForm.gender) {
        errors.gender = 'Gender is required';
      }
      if (!travellerForm.passwordClear) {
        errors.passwordClear = 'Password is required';
      }
      setTravellerFormErrors(errors);
      return Object.keys(errors).length === 0;
    };
  
    const validateLoginForm = () => {
      const errors = {};
      if (!loginForm.email) {
        errors.email = 'Email is required';
      }
      if (!loginForm.password) {
        errors.password = 'Password is required';
      }
      setLoginFormErrors(errors);
      return Object.keys(errors).length === 0;
    };
  
    const registerAsTravelAgent = async (event) => {
      event.preventDefault();
    
      if (!validateAgentForm()) {
        return;
      }
    
      try {
        const response = await fetch('http://localhost:5212/api/UserRegistrationRegisterAsTravelAgent', {
          method: 'POST',
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json'
          },
          body: JSON.stringify({
              travelAgentId: 0,
              users: {
                  userId: 0,
                  email: agentForm.email,
                  passwordHash: '',
                  passwordKey: '',
                  role: 'travelAgent'
              },
              username: agentForm.username,
              email: agentForm.email,
              agencyName: agentForm.agencyName,
              contactNumber: agentForm.contactNumber,
              agentName: agentForm.agentName,
              address: agentForm.address,
              isActive: true,
              passwordClear: agentForm.passwordClear
          })
      });

    
        if (response.ok) {
          navigate('/travelagentdashboard'); // Redirect to Travel Agent Dashboard
        } else {
          // Handling registration error
          console.error('Error registering as Travel Agent');
        }
      } catch (error) {
        console.error('Error registering as Travel Agent:', error);
      }
    
      closeAgentModal();
    };
    const registerAsTraveller = async (event) => {
      event.preventDefault();
    
      if (!validateTravellerForm()) {
        return;
      }
    
      try {
        const response = await fetch('http://localhost:5212/api/UserRegistrationRegisterAsTraveller', {
          method: 'POST',
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json'
          },
          body: JSON.stringify({
              travellerId: 0,
              users: {
                  userId: 0,
                  email: travellerForm.email,
                  passwordHash: '',
                  passwordKey: '',
                  role: 'traveller'
              },
              username: travellerForm.username,
              email: travellerForm.email,
              phoneNumber: travellerForm.phoneNumber,
              gender: travellerForm.gender,
              passwordClear: travellerForm.passwordClear
          })
      });
    
        if (response.ok) {
          navigate('/tourPackages'); 
        } else {
       
          console.error('Error registering as Traveller');
        }
      } catch (error) {
        console.error('Error registering as Traveller:', error);
      }
    
      closeTravellerModal();
    };
        
    const handleLoginSubmit = async (event) => {
      event.preventDefault();
  
      if (!validateLoginForm()) {
        return;
      }
  
      try {
        const response = await fetch('http://localhost:5212/api/UserRegistrationLogin', {
          method: 'POST',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            email: loginForm.email,
            password: loginForm.password
          })
        });
  
        if (response.ok) {
          const userData = await response.json();
  
          if (userData.role === 'Admin') {
            localStorage.setItem('Credentials', JSON.stringify(userData));
            navigate('/adminDashboard'); // Redirect to admin dashboard
          } else if (userData.role === 'TravelAgent') {
            localStorage.setItem('Credentials', JSON.stringify(userData));
            navigate('/travelAgentDashBoard'); // Redirect to travel agent dashboard
          } else if (userData.role === 'traveller') {
            localStorage.setItem('Credentials', JSON.stringify(userData));
            navigate('/traveller'); // Redirect to traveller dashboard
          }
        } else {
          // Handle login error
          console.error('Login failed');
        }
      } catch (error) {
        console.error('Error during login:', error);
      }
  
      closeLoginModal();
    };
    const openAgentModal = () => {
        setIsAgentModalOpen(true);
        setAgentFormErrors({});
      };
      
      const openTravellerModal = () => {
        setIsTravellerModalOpen(true);
        setTravellerFormErrors({});
      };
      
      const openLoginModal = () => {
        setIsLoginModalOpen(true);
        setLoginFormErrors({});
      };
      
    return (

        <div className="landingHeader">
            <div className="landingNavbar">
                <ul className="navList">
                    <li className="logo"><img src={brand} alt="logo" style={{ borderRadius: '50%', objectFit: 'cover', width: '80px', height: '80px' }}/></li>
                    <li onClick={openAgentModal}><BiUser /> RegisterAsTravelAgent</li>
                    <li onClick={openTravellerModal}><BiUser /> RegisterAsTraveller</li>
                    <li onClick={openLoginModal}><BiLock /> Login</li>
                </ul>

            </div>

            <div className="landingdisplayVideo">
                <video className="vid" src={require('../Images/tour.mp4')} autoPlay loop muted playsInline />
                <div className='quote'>
                    <p>We travel not to escape life, but for life not to escape us.</p>
                </div>
            </div>
            <div className='siteIntro'>
                Best Tour Packages we have
            </div>
            <div className="toursimPackageTypes">

                <div className='adventureTourism card'>
                    <img src={adventure} alt="Adventure"></img>
                    <div>More than 40+ adventure tours</div>
                </div>
                <div className='wildLifeTourism card'>
                    <img src={Wild} alt="Wildlife"></img>
                    <div>More than 40+ wildlife tours</div>
                </div>
                <div className='heritageTourism card'>
                    <img src={heritage} alt="Heritage"></img>
                    <div>More than 40+ heritage tours</div>
                </div>
                <div className='premeliageTourism card'>
                    <img src={prilimage} alt="Premier"></img>
                    <div>More than 40+ premier tours</div>
                </div>

            </div>
            <div className='chooseUs'>
                why u need to choose us
            </div>
            <div className='aboutUs'>

                <div className='aboutUscard'>
                    <i class="bi bi-hand-thumbs-up"></i>
                    <h1> 20+ Year Experience</h1>
                    Boasting over
                    two decades in the tourism and
                    hospitality industry,
                    Tour My India has amassed invaluable
                    experience that sets us apart.

                </div>
                <div className='aboutUscard bgColor1'>
                    <i class="bi bi-hand-thumbs-up"></i>
                    <h1>  A Team of Experts</h1>
                    <p>
                        Our experienced
                        team at Tour My India
                        is more than
                        just proficient; they are
                        destination
                        experts for every location
                        within the mesmerizing
                        landscape of India. Their knowledge
                        is an invaluable asset
                        for every traveler.
                    </p>
                </div>
                <div className='aboutUscard bgColor2'>
                    <i class="bi bi-hand-thumbs-up"></i>
                    <h1> Verified Hotels</h1>
                    <p>
                        We offer a meticulously
                        selected
                        and verified list of high-grade hotels.
                        Our partners consistently deliver
                        top-notch service,
                        ensuring an exceptional
                        experience for our guests.
                    </p>
                </div>
                <div className='aboutUscard bgColor3'>
                    <i class="bi bi-hand-thumbs-up"></i>
                    <h1>  Value for Money Tours</h1>
                    <p>
                        With Tour My India,
                        your vacation is not just about sightseeing;
                        it's about creating memories that
                        last a lifetime.
                        Our holiday packages are
                        hassle-free and designed with
                        a focus on value for money.
                        Trust us to weave unforgettable
                        experiences into your journey,
                        ensuring every moment you spend
                        with us is well worth it.
                    </p>
                </div>

            </div>
            <div className='ourAgents'>
                our agents
            </div>
            <div className='Agentsection'>
                <div className='Agentsections'>
                    <div className='cards'>
                        <img src={ag} alt="Adventure" className='agentImage' style={{ borderRadius: '50%', objectFit: 'cover', width: '150px', height: '150px' }} />
                    </div>

                    <div className='cards'>
                        <img src={ag} alt="Adventure" className='agentImage' style={{ borderRadius: '50%', objectFit: 'cover', width: '150px', height: '150px' }} />
                    </div>
                    <div className='cards'>
                        <img src={ag} alt="Adventure" className='agentImage' style={{ borderRadius: '50%', objectFit: 'cover', width: '150px', height: '150px' }} />
                    </div>
                </div>
            </div>
            <div className='footer'>
                <Footer />
            </div>
            <div>      {/* Agent Registration Modal */}
      {isAgentModalOpen && (
        <div className="modalOverlay">
          <div className="modalContent">
            <button className="modalCloseButton" onClick={() => setIsAgentModalOpen(false)}>×</button>
            <h2>Register as Travel Agent</h2>
            <form>
              <div className="inputWithIcon">
                <BiUser className="icon" />
                <input
                  type="text"
                  name="username"
                  placeholder="Username"
                  value={agentForm.username}
                  onChange={handleAgentChange}
                />
                {agentFormErrors.username && <span className="error">{agentFormErrors.username}</span>}
              </div>
              <div className="inputWithIcon">
                <BiEnvelope className="icon" />
                <input
                  type="email"
                  name="email"
                  placeholder="Email"
                  value={agentForm.email}
                  onChange={handleAgentChange}
                />
                {agentFormErrors.email && <span className="error">{agentFormErrors.email}</span>}
              </div>
              <div className="inputWithIcon">
                <BiBuilding className="icon" />
                <input
                  type="text"
                  name="agencyName"
                  placeholder="Agency Name"
                  value={agentForm.agencyName}
                  onChange={handleAgentChange}
                />
                {agentFormErrors.agencyName && <span className="error">{agentFormErrors.agencyName}</span>}
              </div>
              <div className="inputWithIcon">
                <BiPhone className="icon" />
                <input
                  type="text"
                  name="contactNumber"
                  placeholder="Contact Number"
                  value={agentForm.contactNumber}
                  onChange={handleAgentChange}
                />
                {agentFormErrors.contactNumber && <span className="error">{agentFormErrors.contactNumber}</span>}
              </div>
              <div className="inputWithIcon">
                <BiUserPlus className="icon" />
                <input
                  type="text"
                  name="agentName"
                  placeholder="Agent Name"
                  value={agentForm.agentName}
                  onChange={handleAgentChange}
                />
                {agentFormErrors.agentName && <span className="error">{agentFormErrors.agentName}</span>}
              </div>
              <div className="inputWithIcon">
                <BiMap className="icon" />
                <input
                  type="text"
                  name="address"
                  placeholder="Address"
                  value={agentForm.address}
                  onChange={handleAgentChange}
                />
                {agentFormErrors.address && <span className="error">{agentFormErrors.address}</span>}
              </div>
              <div className="inputWithIcon">
                <BiCheckCircle className="icon" />
                <input
                  type="password"
                  name="passwordClear"
                  placeholder="Password"
                  value={agentForm.passwordClear}
                  onChange={handleAgentChange}
                />
                {agentFormErrors.passwordClear && <span className="error">{agentFormErrors.passwordClear}</span>}
              </div>
              <button type="submit" onClick={registerAsTravelAgent}>Register</button>
            </form>
          </div>
        </div>
      )}

     {/* Traveller Registration Modal */}
{isTravellerModalOpen && (
  <div className="modalOverlay">
    <div className="modalContent">
      <button className="modalCloseButton" onClick={() => setIsTravellerModalOpen(false)}>×</button>
      <h2>Register as Traveller</h2>
      <form>
        <div className="inputWithIcon">
          <BiUser className="icon" />
          <input
            type="text"
            name="username"
            placeholder="Username"
            value={travellerForm.username}
            onChange={handleTravellerChange}
          />
          {travellerFormErrors.username && <span className="error">{travellerFormErrors.username}</span>}
        </div>
        <div className="inputWithIcon">
          <BiEnvelope className="icon" />
          <input
            type="email"
            name="email"
            placeholder="Email"
            value={travellerForm.email}
            onChange={handleTravellerChange}
          />
          {travellerFormErrors.email && <span className="error">{travellerFormErrors.email}</span>}
        </div>
        <div className="inputWithIcon">
          <BiPhone className="icon" />
          <input
            type="text"
            name="phoneNumber"
            placeholder="Phone Number"
            value={travellerForm.phoneNumber}
            onChange={handleTravellerChange}
          />
          {travellerFormErrors.phoneNumber && <span className="error">{travellerFormErrors.phoneNumber}</span>}
        </div>
        <div className="inputWithIcon">
          <BiUserPlus className="icon" />
          <select
            name="gender"
            value={travellerForm.gender}
            onChange={handleTravellerChange}
          >
            <option value="">Select Gender</option>
            <option value="Male">Male</option>
            <option value="Female">Female</option>
            <option value="Other">Other</option>
          </select>
          {travellerFormErrors.gender && <span className="error">{travellerFormErrors.gender}</span>}
        </div>
        <div className="inputWithIcon">
          <BiCheckCircle className="icon" />
          <input
            type="password"
            name="passwordClear"
            placeholder="Password"
            value={travellerForm.passwordClear}
            onChange={handleTravellerChange}
          />
          {travellerFormErrors.passwordClear && <span className="error">{travellerFormErrors.passwordClear}</span>}
        </div>
        <button type="submit" onClick={registerAsTraveller}>Register</button>
      </form>
    </div>
  </div>
)}

      {/* Login Modal */}
      {isLoginModalOpen && (
        <div className="modalOverlay">
          <div className="modalContent">
            <button className="modalCloseButton" onClick={() => setIsLoginModalOpen(false)}>×</button>
            <h2>Login</h2>
            <form>
              <div className="inputWithIcon">
                <BiEnvelope className="icon" />
                <input
                  type="email"
                  name="email"
                  placeholder="Email"
                  value={loginForm.email}
                  onChange={handleLoginChange}
                />
                {loginFormErrors.email && <span className="error">{loginFormErrors.email}</span>}
              </div>
              <div className="inputWithIcon">
                <BiLock className="icon" />
                <input
                  type="password"
                  name="password"
                  placeholder="Password"
                  value={loginForm.password}
                  onChange={handleLoginChange}
                />
                {loginFormErrors.password && <span className="error">{loginFormErrors.password}</span>}
              </div>
              <button type="submit" onClick={handleLoginSubmit}>Login</button>
            </form>
          </div>
        </div>
      )}

</div>
            </div>
    );
}
export default LandingPage;