// https://www.npmjs.com/package/react-datepicker
import React from "react";
import PropTypes from "prop-types";
import styles from "../../../index.css";
import ActionsBar from "../../common/ActionsBar";


const OrdersList = ({ orders, accountId, isAccountOrderExist, openModal, deleteOrder,showDetails })=>{
    
    const isOrdersExist = orders?.length > 0;
    const formatedOrders = isOrdersExist ? orders.map(o => { return { orderId: o.orderId, name: o.name, bookingDate: new Date(o.bookingDate + 'Z').toLocaleString('he-IL') }}) : null

    return (<div className="container-fluid">

    <div className="actions-bar"> 
        <ActionsBar isAccountOrderExist = {isAccountOrderExist} AddOrder={openModal} deleteOrder={deleteOrder} />
    </div>

    {(!isOrdersExist) ? (<h3> No schedules for a haircut yet... </h3>) : (
        <table>
            <tbody>
                <tr>
                    <th>Client Name</th>
                    <th>Scheduled Haircut</th>
                    <th></th>
                </tr>
                {isOrdersExist && formatedOrders.map((o,i) => (<tr key={o.orderId}>
                                                                            <td>{o.name}</td>
                                                                            <td>{o.bookingDate}</td>
                                                                            <td><button onClick={()=>showDetails(o.orderId)} className="btn btn-primary">details</button></td>
                                                        </tr>))}
            </tbody>
        </table>
    )}
    </div>);
};

OrdersList.propTypes = {
    orders:PropTypes.array,
    accountId: PropTypes.number
};

export default OrdersList;