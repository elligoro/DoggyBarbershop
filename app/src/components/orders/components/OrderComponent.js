import React, { useState, useEffect } from 'react';
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";

const OrderComponent = (props)=>{
    const currDate = new Date();
    const currTime = getTimeStringFormat(new Date());
    const [date, setDate] = useState(currDate);
    const [time, setTime] = useState(currTime);
    const {order, onSubmit,onClose} = props;
    const [orderId,setOrderId] = useState(null);
    const [errors,setErrors] = useState([]);

    useEffect(()=>{
        var order = props.order();
        setDate(new Date(order.bookingDate));
        setTime(getTimeStringFormat(new Date(order.bookingDate)));
        setOrderId(order.orderId);
    },[]);

    function getTimeStringFormat(time){
        return time.toTimeString().split(" ")[0].split(":").filter((t,i) => i != 2).join(":");
    }

    function handleDateChange(date){
        if(new Date() > date)
        return setErrors("can't schedule order with past time");
        else
            setDate(date);
    }

    function handleTimeChange(e){
        let time = e.target.value;
        validateTime(time);
        setTime(time);
    }

    function validateTime(time){
        setErrors("");

        if(time.length < 5)
            return setTime(time);
        let testTime = time.match(/^([01]?[0-9]|2[0-3]):[0-5][0-9]$/);
        if(!(time && testTime))
            return setErrors("wrong time format");

        const nowTime = getTimeStringFormat(new Date());
        const nowTimeArr = nowTime.split(":");
        const timeArr = time.split(":");
        if((currDate.toLocaleDateString() == date.toLocaleDateString()) 
            && (nowTimeArr[0] > timeArr[0] || (nowTimeArr[0] == timeArr[0] && nowTimeArr[1] > timeArr[1])))
            return setErrors("can't schedule order with past time");
    }

    function handleOnSubmit(e){
        const timeArr = time.split(":");
        console.log(date);
        const savedDate = new Date(date.getFullYear(),
                                    date.getMonth(),
                                    date.getDate(),
                                    timeArr[0],
                                    timeArr[1]); 

        console.log(savedDate);
        props.onSubmit({
                        bookingDate: savedDate,
                        orderId: orderId});
    }

    return (
        <>        
        <div className="form-container">
            <h3>shcedule your dogs haircut</h3>
                <div className="form-field">
                <label>
                    Date for haircut:
                </label>
                <DatePicker selected={date} onChange={handleDateChange} />
                <label>
                    Time for haircut (hh:mm):
                </label>
                <input value={time} onChange={handleTimeChange}/>
                </div>
                <div className="form-field">
                    <button className="btn btn-primary submit" onClick={handleOnSubmit}>Submit</button>
                </div>
        </div>
        <button className="btn btn-primary close" onClick={onClose}>Close</button>

        {(errors && errors.length > 0) ? 
            (<div className="alerts">
                <p>There is an issue with date:</p>
                <p>{errors}</p>
            </div>) : <></>
        }
        </>
    );

}


export default OrderComponent;