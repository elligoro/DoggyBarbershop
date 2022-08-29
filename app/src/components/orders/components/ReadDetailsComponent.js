import React, { useState, useEffect } from 'react';
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";

const ReadDetailsComponent = (props)=>{
    const {order, onClose} = props;

    return (
        <>
        <div className="form-container">
            <h3>Haircut details:</h3>
                <div className="form-field">
                <label>Name:</label>
                <p>{order.name}</p>
                <label>Scheduled date:</label>
                <p>{new Date(order.bookingDate + 'Z').toLocaleString('he-IL')}</p>
                <label>Created Date:</label>
                <p>{new Date(order.createdDate + 'Z').toLocaleString('he-IL')}</p>
                </div>
        </div>
        
        <button className="btn btn-primary close" onClick={onClose}>Close</button>
        </>
    );

}


export default ReadDetailsComponent;