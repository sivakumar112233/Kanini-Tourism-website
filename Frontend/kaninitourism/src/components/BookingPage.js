import React, { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';
import './BookingPage.css';
import { Link, useParams } from 'react-router-dom';
import { PDFDownloadLink, Document, Page, Text, View, StyleSheet } from '@react-pdf/renderer';
import Navbar from './Navbar';
import Footer from './Footer';

function BookingPage() {
  const { tourId, tourPrice, maxCount } = useParams();
  const tourData = useSelector((state) => state.tours.tourData);
  const [storedObject, setStoredObject] = useState(null);
  const selectedTour= tourData.find((tour) => tour.tourId === parseInt(tourId));
  const [bookingDate, setBookingDate] = useState(new Date());
  const [bookingCount, setBookingCount] = useState(0);
  const [noOfPersons, setNoOfPersons] = useState(1);
  const [bookingUsers, setBookingUsers] = useState([]);
  const [availableSlots, setAvailableSlots] = useState(maxCount - bookingCount);
  const [showPdfLink, setShowPdfLink] = useState(false);
  const [travellerId,setTravellerId] = useState(null);

    console.log(travellerId)
  useEffect(() => {
    checkAvailability();
  }, []);


  const checkAvailability = async () => {
    try {
      const response = await fetch(`http://localhost:5103/api/ToursimBooking?tourId=${tourId}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (response.status === 200) {
        const data = await response.json();
        const remainingSlots = maxCount - data.bookingCount;
        setAvailableSlots(remainingSlots);
      } else {
        // Handle error cases
      }
    } catch (error) {
      console.error(error);
    }
  };

  const handleUserChange = (index, field, value) => {
    setBookingUsers(prevUsers => {
      const updatedUsers = [...prevUsers];
      if (!updatedUsers[index]) {
        updatedUsers[index] = {};
      }
      updatedUsers[index][field] = value;
      return updatedUsers;
    });
  };

  const handlePersonChange = (e) => {
    const persons = parseInt(e.target.value);
    setNoOfPersons(persons);
    const remainingSlots = maxCount - bookingCount;
    setAvailableSlots(remainingSlots);
    setBookingDate(new Date());
    const newBookingUsers = Array.from({ length: persons }, () => ({
      bookingUserName: '',
      bookingUserGender: '',
      bookingUserPhoneNumber: '',
      bookingUserEmail: '',
    }));
    setBookingUsers(newBookingUsers);
  };

  const handleSubmit = async () => {
    setTravellerId(localStorage.getItem("userId")); 
    console.log(travellerId);
    try {
      const bookingData = {
        travellerId:43,
        tourId: parseInt(tourId),
        noOfPersons: noOfPersons,
        bookingDate: bookingDate.toISOString(),
        bookingUsers: bookingUsers,
      };
      console.log(bookingData);
      const response = await fetch(`http://localhost:5103/api/ToursimBooking`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({...bookingData}),
      });

      if (response.status === 201) {
        const responseData = await response.json();
        setShowPdfLink(true);
        setBookingDate(new Date());
      setNoOfPersons(1);
      setBookingUsers([]);
        
      } else {
        console.log(response);
      }
    } catch (error) {
      console.error(error);
    }
  };

  const MyDocument = () => (
    <Document>
      <Page style={styles.page}>
        <View style={styles.section}>

          <Text style={styles.textOrange}>Price per person: ${tourPrice*noOfPersons}</Text>
          <Text style={styles.textOrange}>Booking Date: {bookingDate.toISOString().substr(0, 10)}</Text>
          <Text style={styles.textOrange}>Number of Persons: {noOfPersons}</Text>
        </View>
        {bookingUsers.map((user, index) => (
          <View key={index} style={styles.section}>
            <Text>Person {index + 1}</Text>
            <Text>Name: {user.bookingUserName}</Text>
            <Text>Gender: {user.bookingUserGender}</Text>
            <Text>Phone: {user.bookingUserPhoneNumber}</Text>
            <Text>Email: {user.bookingUserEmail}</Text>
            <Text>Price: ${noOfPersons * tourPrice}</Text>
          </View>
        ))}
      </Page>
    </Document>
  );

  const styles = StyleSheet.create({
    page: {
      flexDirection: 'column',
      backgroundColor: '#ffffff',
      padding: '1cm',
      border: '1px solid'
    },
    section: {
      marginBottom: 10,
    },

  });


  const [showAvailabilityModal, setShowAvailabilityModal] = useState(false);

  const openAvailabilityModal = () => {
    setShowAvailabilityModal(true);
  };

  const closeAvailabilityModal = () => {
    setShowAvailabilityModal(false);
  };

  return (
    <div>
      <Navbar />
      <div className="bookingPageContainer">
        <h2>Booking Page</h2>


        <p className="tourInfo"> hey before booking please check availability</p>
        <button onClick={openAvailabilityModal} className="check-availability-button">
          Check Availability
        </button>
        <br />
        {showAvailabilityModal && (
          <div className="overlay">
            <div className="availabilityModal">
              <h2>Available Slots</h2>
              <p>{availableSlots} slots available</p>
              <button onClick={closeAvailabilityModal}>Close</button>
            </div>
          </div>
        )}
        <label>
          <span className="inputFieldLabel">Booking Date:</span>
          <input
            type="date"
            value={bookingDate.toISOString().substr(0, 10)}
            onChange={(e) => setBookingDate(new Date(e.target.value))}
            className="inputField"
          />
        </label>
        <label>
          Number of Persons:
          <input
            type="number"
            value={noOfPersons}
            onChange={handlePersonChange}
            className="inputField"
          />
        </label>
        <div> 
          {Array.from({ length: noOfPersons }, (_, index) => (
            <div key={index} className="userForm">
              <h3>Person {index + 1}</h3>
              <label>
                Name:
                <input
                  type="text"
                  value={bookingUsers[index]?.bookingUserName || ''}
                  onChange={(e) =>
                    handleUserChange(index, 'bookingUserName', e.target.value)
                  }
                  className="inputField"
                />
              </label>
              <label>
                Gender:
                <input
                  type="text"
                  value={bookingUsers[index]?.bookingUserGender || ''}
                  onChange={(e) =>
                    handleUserChange(index, 'bookingUserGender', e.target.value)
                  }
                  className="inputField"
                />
              </label>
              <label>
                Phone:
                <input
                  type="text"
                  value={bookingUsers[index]?.bookingUserPhoneNumber || ''}
                  onChange={(e) =>
                    handleUserChange(index, 'bookingUserPhoneNumber', e.target.value)
                  }
                  className="inputField"
                />
              </label>
              <label>
                Email:
                <input
                  type="text"
                  value={bookingUsers[index]?.bookingUserEmail || ''}
                  onChange={(e) =>
                    handleUserChange(index, 'bookingUserEmail', e.target.value)
                  }
                  className="inputField"
                />
              </label>
            </div>
          ))}
        </div>
        <button onClick={handleSubmit} className="submitButton">
          Submit Booking
        </button>
        <span style={{"textDecoration":"none"}}>
        {showPdfLink && (
          <PDFDownloadLink document={<MyDocument bookingData={null} />} fileName="booking.pdf">
            {({ loading }) => (loading ? 'Loading document...' : 'Download InVoice')}
          </PDFDownloadLink>
        )}
        </span>
        {noOfPersons > availableSlots && (
          <p>Only {availableSlots} slots left. Please select a lower number of persons or check again later.</p>
        )}

        <Link to={`/tour-details/${tourId}`} className="backLink">
          Back to Tour Details
        </Link>
      </div>
      <div>
        <Footer />
      </div>
    </div>
  );
}

export default BookingPage;
