import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom"; // Note the use of Routes
import FeedBackPage from './components/FeedBackPage';
import LandingPage from './components/LandingPage';
import TourPackages from './components/TourPackages';
import TourDetails from './components/TourDetails';
import BookingPage from './components/BookingPage';
import Userprofile from './components/UserProfile';
import ContactPage from './components/ContactPage';
import AboutUs from './components/AboutUs';
import TravelagentDashBoard from './components/TravelAgentDashBoard';
import AdminDashboard from './components/AdminDashboard';
import ProtectedRoutes from './components/ProtectedRoutes';
import UnauthorizedPage from './components/UnauthorizedPage';


function App() {
   return (
   <BrowserRouter>
      <Routes>
      <Route  path="/" element={<LandingPage/>} />
        <Route path="/tour-details/:tourId" element={<TourDetails/>} />
        <Route exact path="/booking/:tourId/:tourPrice/:maxCount" element={<BookingPage/>} />
        <Route path="/feedback" element={<FeedBackPage />} />
        <Route path="/userprofile" element={<Userprofile/>}/>
        <Route path="/contact" element={<ContactPage/>}/>
        <Route path="/about" element={<AboutUs/>}/>
        <Route path="/tourPackages"element={<TourPackages/>}/>
        <Route path="/travelagentdashboard" element={<ProtectedRoutes element={<TravelagentDashBoard />} allowedRoles={['TravelAgent']} />} />
        <Route path="/adminDashboard" element={<ProtectedRoutes element={<AdminDashboard />} allowedRoles={['Admin']} />} />
        <Route path="/unauthorized" element={<UnauthorizedPage />} />
     </Routes>
   </BrowserRouter>

 



 

  );
}

export default App;
