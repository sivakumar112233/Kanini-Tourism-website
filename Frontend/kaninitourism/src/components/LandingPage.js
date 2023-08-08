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
    const navigate = useNavigate();
    const openAgentModal = () => {
        setIsAgentModalOpen(true);
    };

    const closeAgentModal = () => {
        setIsAgentModalOpen(false);
    };

    const openTravellerModal = () => {
        setIsTravellerModalOpen(true);
    };

    const closeTravellerModal = () => {
        setIsTravellerModalOpen(false);
    };

    // Travel Agent Registration Form State
    const [agentForm, setAgentForm] = useState({
        username: '',
        email: '',
        agencyName: '',
        contactNumber: '',
        agentName: '',
        address: '',
        passwordClear: ''
    });

    // Traveller Registration Form State
    const [travellerForm, setTravellerForm] = useState({
        username: '',
        email: '',
        phoneNumber: '',
        gender: '',
        passwordClear: ''
    });






    const handleAgentChange = (event) => {
        const { name, value } = event.target;
        setAgentForm({
            ...agentForm,
            [name]: value
        });
    };

    const handleTravellerChange = (event) => {
        const { name, value } = event.target;
        setTravellerForm({
            ...travellerForm,
            [name]: value
        });
    };

    const registerAsTravelAgent = async (event) => {
        event.preventDefault();
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
                        role: 'travel_agent'
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

            const data = await response.json();
            console.log('Travel Agent Registration Response:', data);

            if (response.ok) {
                navigate('/travelagentdashboard'); // Redirect to Travel Agent Dashboard
            }

            closeAgentModal();
        } catch (error) {
            console.error('Error registering as Travel Agent:', error);
        }
    };

    const registerAsTraveller = async (event) => {
        event.preventDefault();
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

            const data = await response.json();
            console.log('Traveller Registration Response:', data);

            if (response.ok) {
                navigate('/tourPackages');
                // Redirect to Traveller Dashboard
            }

            closeTravellerModal();
        } catch (error) {
            console.error('Error registering as Traveller:', error);
        }
    };
    const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
    const [loginForm, setLoginForm] = useState({
        email: '',
        password: ''
    });

    const openLoginModal = () => {
        setIsLoginModalOpen(true);
    };

    const closeLoginModal = () => {
        setIsLoginModalOpen(false);
    };

    const handleLoginChange = (event) => {
        const { name, value } = event.target;
        console.log(name, value)
        setLoginForm({
            ...loginForm,
            [name]: value
        });
    };

    const handleLoginSubmit = async (event) => {

        event.preventDefault();


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

                // Get the navigation function

                if (userData.role === 'Admin') {
                    localStorage.setItem('Credentials', JSON.stringify(userData));
                    navigate('/adminDashboard'); // Redirect to admin dashboard

                } else if (userData.role === 'TravelAgent') {
                    localStorage.setItem('Credentials', JSON.stringify(userData));
                    navigate('/travelAgentDashBoard'); // Redirect to travel agent dashboard
                } else if (userData.role === 'traveller') {
                    localStorage.setItem('Credentials', JSON.stringify(userData));
                    navigate('/travelerdashboard'); // Redirect to traveller dashboard
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
            <div> {/* Travel Agent Registration Modal */}
                {isAgentModalOpen && (
                    <div className="modalOverlay">
                        <div className="modalContent">
                            <button className="modalCloseButton" onClick={closeAgentModal}>×</button>
                            <h2>Register as Travel Agent</h2>
                            <form>
                                <div className="inputWithIcon">
                                    <BiUser className="icon" />
                                    <input type="text" name="username" placeholder="Username" value={agentForm.username} onChange={handleAgentChange} />
                                </div>
                                <div className="inputWithIcon">
                                    <BiEnvelope className="icon" />
                                    <input type="email" name="email" placeholder="Email" value={agentForm.email} onChange={handleAgentChange} />
                                </div>
                                <div className="inputWithIcon">
                                    <BiBuilding className="icon" />
                                    <input type="text" name="agencyName" placeholder="Agency Name" value={agentForm.agencyName} onChange={handleAgentChange} />
                                </div>
                                <div className="inputWithIcon">
                                    <BiPhone className="icon" />
                                    <input type="text" name="contactNumber" placeholder="Contact Number" value={agentForm.contactNumber} onChange={handleAgentChange} />
                                </div>
                                <div className="inputWithIcon">
                                    <BiUserPlus className="icon" />
                                    <input type="text" name="agentName" placeholder="Agent Name" value={agentForm.agentName} onChange={handleAgentChange} />
                                </div>
                                <div className="inputWithIcon">
                                    <BiMap className="icon" />
                                    <input type="text" name="address" placeholder="Address" value={agentForm.address} onChange={handleAgentChange} />
                                </div>
                                <div className="inputWithIcon">
                                    <BiCheckCircle className="icon" />
                                    <input type="password" name="passwordClear" placeholder="Password" value={agentForm.passwordClear} onChange={handleAgentChange} />
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
                            <button className="modalCloseButton" onClick={closeTravellerModal}>×</button>
                            <h2>Register as Traveller</h2>
                            <form>
                                <div className="inputWithIcon">
                                    <BiUser className="icon" />
                                    <input type="text" name="username" placeholder="Username" value={travellerForm.username} onChange={handleTravellerChange} />
                                </div>
                                <div className="inputWithIcon">
                                    <BiEnvelope className="icon" />
                                    <input type="email" name="email" placeholder="Email" value={travellerForm.email} onChange={handleTravellerChange} />
                                </div>
                                <div className="inputWithIcon">
                                    <BiPhone className="icon" />
                                    <input type="text" name="phoneNumber" placeholder="Phone Number" value={travellerForm.phoneNumber} onChange={handleTravellerChange} />
                                </div>
                                <div className="inputWithIcon">
                                    <BiUserPlus className="icon" />
                                    <input type="text" name="gender" placeholder="Gender" value={travellerForm.gender} onChange={handleTravellerChange} />
                                </div>
                                <div className="inputWithIcon">
                                    <BiCheckCircle className="icon" />
                                    <input type="password" name="passwordClear" placeholder="Password" value={travellerForm.passwordClear} onChange={handleTravellerChange} />
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
                            <button className="modalCloseButton" onClick={closeLoginModal}>×</button>
                            <h2>login</h2>
                            <form onSubmit={handleLoginSubmit}>
                                <div className="inputWithIcon">
                                    <BiEnvelope className="icon" />
                                    <input type="string" name="email" placeholder="Email" value={loginForm.email} onChange={handleLoginChange} />
                                </div>
                                <div className="inputWithIcon">
                                    <BiLock className="icon" />
                                    <input type="string" name="password" placeholder="Password" value={loginForm.password} onChange={handleLoginChange} />
                                </div>
                                <button type="submit">Login</button>
                            </form>

                        </div>
                    </div>
                )}



            </div>

        </div>
    );
}

export default LandingPage;
