import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Dropzone from 'react-dropzone';
import './TravelAgentDashboard.css';
import {
  FaUser,
  FaInfoCircle,
  FaTrash,
  FaPhoneAlt,
  FaCheckCircle,
  FaTimesCircle,
  FaSignOutAlt
} from 'react-icons/fa';
import { MdAttachEmail } from 'react-icons/md';
import Footer from './Footer';
import { useNavigate } from 'react-router-dom';


function TravelAgentDashboard() {
  const [formStep, setFormStep] = useState(1);
  const [showProfilePopup, setShowProfilePopup] = useState(false);
  const [travelAgentInfo, setTravelAgentInfo] = useState(null);
  const [addedTourId, setAddedTourId] = useState(null);
  const [formSubmitted, setFormSubmitted] = useState(false);
  const navigate = useNavigate();
  const [tourPackage, setTourPackage] = useState({
    tourId: 0,
    tourName: '',
    tourPrice: '',
    tourLocationCountry: '',
    tourLocationState: '',
    tourLocationCity: '',
    tourSpecialty: '',
    maxCount: '',
    inclusion: {
      inclusionId: 0,
      meals: '',
      accommodation: '',
      transfer: '',
      totalNights: '',
      totalDays: '',
      totalDaysDescriptions: [],
    },
    travelAgentId: 0,
    images: [],
  });
  useEffect(() => {
    fetchTravelAgentInfo();
  }, []);
  const handleLogout = () => {

    localStorage.removeItem('Credentials');
    navigate('/');
  };
  const handleImageDrop = async () => {
    if (formSubmitted) {
      const formData = new FormData();
    
      tourPackage.images.forEach((file) => {
        formData.append('Images', file);
      });
    
      formData.append('TourId', addedTourId);
    
      try {
        const response = await axios.post(
          'http://localhost:5287/api/TourImages/CreateTour',
          formData,
          {
            headers: {
              'Content-Type': 'multipart/form-data',
            },
          }
        );
  
        if (response.status === 200) {
          alert('Images uploaded successfully!');
          
          // Reset form step and other states
          setFormStep(1);
          setFormSubmitted(false);
          setAddedTourId(null);
          setTourPackage({
            tourId: 0,
            tourName: '',
            tourPrice: '',
            tourLocationCountry: '',
            tourLocationState: '',
            tourLocationCity: '',
            tourSpecialty: '',
            maxCount: '',
            inclusion: {
              inclusionId: 0,
              meals: '',
              accommodation: '',
              transfer: '',
              totalNights: '',
              totalDays: '',
              totalDaysDescriptions: [],
            },
            travelAgentId: 0,
            images: [],
          });
        }
      } catch (error) {
        console.error('Error uploading images:', error);
      }
    }
  };
  
  
  const handleFormSubmit = async () => {
    try {
      console.log(tourPackage, "tourpackage");
      console.log("Starting form submission...");
      const response = await axios.post(
        'http://localhost:5299/api/Tour/AddingANewTourPackage',
        tourPackage
      );

      console.log('Tour Package Data:', tourPackage);
      console.log("Tour package submitted:", response.data);

      // If the tour creation is successful, setting the added tour ID and show a popup
      if (response.status === 201) {
        setFormSubmitted(true);
        const newTourId = response.data.tourId; // Extracting the new tourId
        setAddedTourId(newTourId);
        alert('Tour package added successfully! You can now add images.');
        setFormStep(5); // Move to the image upload step

        // Calling handleImageDrop here with the selected images
        handleImageDrop(tourPackage.images);

        // Updating the tourPackage state with the new tourId
        setTourPackage((prevPackage) => ({
          ...prevPackage,
          tourId: newTourId,
        }));
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };



  const fetchTravelAgentInfo = async () => {
    try {
      const response = await axios.get('http://localhost:5212/api/UserRegistrationGettingTravelAgentDetails?index=8');
      setTravelAgentInfo(response.data);

    } catch (error) {
      console.error('Error fetching travel agent information:', error);
    }
  };
  const handleInclusionChange = (event) => {
    const { name, value } = event.target;
    setTourPackage((prevPackage) => ({
      ...prevPackage,
      inclusion: {
        ...prevPackage.inclusion,
        [name]: value,
      },
    }));
  };
  const handleDayDescriptionChange = (event, index, field) => {
    const { value } = event.target;
    setTourPackage((prevPackage) => ({
      ...prevPackage,
      inclusion: {
        ...prevPackage.inclusion,
        totalDaysDescriptions: prevPackage.inclusion.totalDaysDescriptions.map((desc, i) =>
          i === index ? { ...desc, [field]: value } : desc
        ),
      },
    }));
  };
  const handleTotalDaysChange = (event) => {
    const totalDays = parseInt(event.target.value);
    const totalDaysDescriptions = new Array(totalDays).fill('').map((_, index) => ({
      totalDaysDescriptionId: index,
      tourSpotName: '',
      inclusionId: 0,
      dayDescription: '',
    }));

    setTourPackage((prevPackage) => ({
      ...prevPackage,
      inclusion: {
        ...prevPackage.inclusion,
        totalDays,
        totalDaysDescriptions,
      },
    }));
  };

  const handleDeleteImage = (index) => {
    const shouldDelete = window.confirm("Are you sure you want to delete this image?");
    if (shouldDelete) {
      const newImages = tourPackage.images.filter((_, i) => i !== index);
      setTourPackage((prevPackage) => ({
        ...prevPackage,
        images: newImages,
      }));
    }
  };


  const renderForm = () => {
    switch (formStep) {
      case 1:
        return (
          <div className="formStep">
            <h2>Basic Package Information</h2>
            <input
              type="text"
              name="tourName"
              placeholder="Tour Name"
              value={tourPackage.tourName}
              onChange={handleInputChange}
            />
            <input
              type="number"
              name="tourPrice"
              placeholder="Tour Price"
              value={tourPackage.tourPrice}
              onChange={(event) => { setTourPackage({ ...tourPackage, "tourPrice": Number(event.target.value) }) }}
            />
            <input
              type="text"
              name="tourLocationCountry"
              placeholder="Country"
              value={tourPackage.tourLocationCountry}
              onChange={handleInputChange}
            />
            <input
              type="text"
              name="tourLocationState"
              placeholder="State"
              value={tourPackage.tourLocationState}
              onChange={handleInputChange}
            />
            <input
              type="text"
              name="tourLocationCity"
              placeholder="City"
              value={tourPackage.tourLocationCity}
              onChange={handleInputChange}
            />
            <input
              type="text"
              name="tourSpecialty"
              placeholder="Specialty"
              value={tourPackage.tourSpecialty}
              onChange={handleInputChange}
            />
            <input
              type="number"
              name="maxCount"
              placeholder=" Max Count"
              value={tourPackage.maxCount}
              onChange={(event) => { setTourPackage({ ...tourPackage, "maxCount": Number(event.target.value) }) }}
            />
            <button onClick={() => setFormStep(2)}>Next</button>
          </div>
        );
      case 2:
        return (
          <div className="formStep">
            <h2>Inclusion Information</h2>
            <input
              type="text"
              name="meals"
              placeholder="Meals"
              value={tourPackage.inclusion.meals}
              onChange={handleInclusionChange}
            />
            <input
              type="text"
              name="accommodation"
              placeholder="Acommodation"
              value={tourPackage.inclusion.accommodation}
              onChange={handleInclusionChange}
            />
            <input
              type="text"
              name="transfer"
              placeholder="Transfer"
              value={tourPackage.inclusion.transfer}
              onChange={handleInclusionChange}
            />
            <input
              type="number"
              name="totalNights"
              placeholder="Total Nights"
              value={tourPackage.inclusion.totalNights}
              onChange={(event) => { setTourPackage({ ...tourPackage, "inclusion": { ...tourPackage.inclusion, "totalNights": Number(event.target.value) } }) }}
            />
            <input
              type="number"
              name="totalDays"
              placeholder="Total Days"
              value={tourPackage.inclusion.totalDays}
              onChange={handleTotalDaysChange}
            />
            <div>
            <button onClick={() => setFormStep(1)}>Back</button>
            <button onClick={() => setFormStep(3)}>Next</button>
            </div>
          </div>
        );
      case 3:
        return (
          <div className="formStep">
            <h2>Day Descriptions</h2>
            {tourPackage.inclusion.totalDaysDescriptions.map((desc, index) => (
              <div key={index}>
                <h3>Day {index + 1}</h3>
                <input
                  type="text"
                  name={`tourSpotName_${index}`}
                  placeholder={`Tour Spot Name`}
                  value={desc.tourSpotName}
                  onChange={(event) => handleDayDescriptionChange(event, index, 'tourSpotName')}
                />
                <input
                  type="text"
                  name={`dayDescription_${index}`}
                  placeholder={`Day ${index + 1} Description`}
                  value={desc.dayDescription}
                  onChange={(event) => handleDayDescriptionChange(event, index, 'dayDescription')}
                />
              </div>
            ))}
            <div>            
              <button onClick={() => setFormStep(2)}>Back</button>
              <button onClick={() => setFormStep(4)}>Next</button>
            </div>
          </div>
        );
      case 4:
        return (
          <div className="formStep ">
            <h2>Review and Submit</h2>
            <div className="reviewSection">
              <h3>Basic Package Information</h3>
              <p>Tour Name: {tourPackage.tourName}</p>
              <p>Tour Price: {tourPackage.tourPrice}</p>
              <p>Country: {tourPackage.tourLocationCountry}</p>
              <p>State: {tourPackage.tourLocationState}</p>
              <p>City: {tourPackage.tourLocationCity}</p>
              <p>Specialty: {tourPackage.tourSpecialty}</p>
              <p>Max Count: {tourPackage.maxCount}</p>
            </div>
            <div className="reviewSection">
              <h3>Inclusion Information</h3>
              <p>Meals: {tourPackage.inclusion.meals}</p>
              <p>Transfer: {tourPackage.inclusion.transfer}</p>
              <p>Total Nights: {tourPackage.inclusion.totalNights}</p>
              <p>Total Days: {tourPackage.inclusion.totalDays}</p>
            </div>
            <div className="reviewSection dayDescriptions">
              <h3>Day Descriptions</h3>
              {tourPackage.inclusion.totalDaysDescriptions.map((desc, index) => (
                <div key={index}>
                  <h4>Day {index + 1}</h4>
                  <p>Tour Spot Name: {desc.tourSpotName}</p>
                  <p>Day Description: {desc.dayDescription}</p>
                </div>
              ))}
            </div>
            <button onClick={() => setFormStep(3)}>Back</button>
            <button onClick={handleFormSubmit}>Submit</button>
          </div>
        );

        case 5:
  return (
    <div className="formStep">
      <h2>Add Images</h2>
      
      <Dropzone onDrop={acceptedFiles => setTourPackage({...tourPackage, images: acceptedFiles})} accept="image/*" multiple>
        {({ getRootProps, getInputProps }) => (
          <div className="dropzone" {...getRootProps()}>
            <input {...getInputProps()} />
            <p>Drag & drop images here, or click to select files</p>
          </div>
        )}
      </Dropzone>
      
      <div className="droppedImages">
        {tourPackage.images.map((image, index) => (
          <div key={index} className="droppedImage">
            <img src={URL.createObjectURL(image)} alt={`Dropped ${index}`} />
            <button className="deleteButton" onClick={() => handleDeleteImage(index)}>
              <FaTrash />
            </button>
          </div>
        ))}
      </div>
      
     
      <button onClick={() => handleImageDrop()}>Submit</button>

      
    </div>
  );

      default:
        return null;
    }
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setTourPackage((prevPackage) => ({
      ...prevPackage,
      [name]: value,
    }));
  };

  return (
    <div className="dashboardContainer">
      <nav className="topNavbar">
        <div className="logo">KaniniTourismTravelAgentDashboard</div>
        <div className="profile" onClick={() => setShowProfilePopup(true)}>
          <FaUser />
        </div>
      </nav>
      <div className="packageCard">
        <div className="cardHeader">
          <h2>Add a Package</h2>
        </div>
        {travelAgentInfo && travelAgentInfo.isActive ? (
          <div className="cardContent">{renderForm()}</div>
        ) : (
          <div className="cardContent">
            <p>Please contact admin to add a package.</p>
          </div>
        )}
      </div>
      {showProfilePopup && (
        <div className="profilePopup">
          <div className="popupContent">
            <h2>Travel Agent Information</h2>
            {travelAgentInfo && (
              <div>
                <br />
                <p>
                  <FaUser /> Username: {travelAgentInfo.username}<br />
                </p>
                <br />
                <p>
                  <MdAttachEmail /> Email: {travelAgentInfo.email}<br />
                </p>
                <br />
                <p>
                  <FaUser />  Agency Name: {travelAgentInfo.agencyName}<br />
                </p>
                <br />
                <p>
                  <FaPhoneAlt /> Phone Number: {travelAgentInfo.phoneNumber}<br />
                </p>
                <br />
                <p>
                  {travelAgentInfo.isActive ? <FaCheckCircle /> : <FaTimesCircle />} IsActive
                </p>
                <br />
                <button className="logoutButton" style={{ "color": "orange" }} onClick={handleLogout}>
                  <FaSignOutAlt /> Logout
                </button>
              </div>
            )}
            <button onClick={() => setShowProfilePopup(false)}>Close</button>
          </div>
        </div>
      )}
      <Footer />
    </div>
  );
}

export default TravelAgentDashboard;