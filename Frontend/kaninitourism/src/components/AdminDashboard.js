// AdminDashboard.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './AdminDashboard.css';
import Footer from './Footer';
import { useNavigate } from 'react-router-dom';
import { FaSignOutAlt } from 'react-icons/fa';

function AdminDashboard() {
  const [travelAgents, setTravelAgents] = useState([]);
  const navigate = useNavigate();
 
  useEffect(() => {
    fetchTravelAgents();
  }, []);
  const handleLogout = () => {
  
    localStorage.removeItem('adminCredentials');
    navigate('/');
  };
  const fetchTravelAgents = async () => {
    try {
      const response = await axios.get(
        'http://localhost:5212/api/UserRegistrationGetAllTravelAgents'
      );
      setTravelAgents(response.data);
    } catch (error) {
      console.error('Error fetching travel agents:', error);
    }
  };

  const handleStatusClick = async (agentId, isActive) => {
    if (!isActive) {
      try {
        const response = await axios.post(
          'http://localhost:5212/api/UserRegistrationUpdateTravelAgentStatus',
          {
            travelAgentId: agentId,
            isActive: true,
          }
        );
        if (response.status === 200) {
          fetchTravelAgents(); 
        }
      } catch (error) {
        console.error('Error updating travel agent status:', error);
      }
    }
  };

  return (
    <div className="adminContainer">
      <div className="header">
        <div className="adminNavbar">
          <div className="navbarTitle">Kanini Tourism AdminDashboard</div>
          <button onClick={handleLogout} className="logoutButton">
  <FaSignOutAlt className="logoutIcon" />
  Logout
</button>

        </div>
      </div>
      <div className="tableContainer">
        <table className="travelAgentTable">
          <thead>
            <tr>
              <th>Username</th>
              <th>Email</th>
              <th>Agency Name</th>
              <th>Contact Number</th>
              <th>Agent Name</th>
              <th>Address</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            {travelAgents.map((agent) => (
              <tr key={agent.travelAgentId}>
                <td>{agent.username}</td>
                <td>{agent.email}</td>
                <td>{agent.agencyName}</td>
                <td>{agent.contactNumber}</td>
                <td>{agent.agentName}</td>
                <td>{agent.address}</td>
                <td>
                  <button
                    className={`statusButton ${agent.isActive ? 'active' : 'inactive'}`}
                    onClick={() => handleStatusClick(agent.travelAgentId, agent.isActive)}
                    disabled={agent.isActive} 
                  >
                    {agent.isActive ? 'Active' : 'Inactive'}
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <Footer className="footers" />
    </div>
  );
}

export default AdminDashboard;
