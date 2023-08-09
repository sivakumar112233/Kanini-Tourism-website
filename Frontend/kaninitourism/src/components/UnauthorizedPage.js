import React from 'react';
import { FaLock } from 'react-icons/fa';
import './UnauthorizedPage.css';

function UnauthorizedPage() {
    return (
        <div className="forbidden">
            <div className="centerContent">
                <p className="errorText">You are unauthorized to access this page.</p>
                <FaLock className="lockIcon" />
            </div>
        </div>
    );
}

export default UnauthorizedPage;
