import React, {useState,useEffect} from 'react'; 

const ActionsBar = (props)=>{
    const {isAccountOrderExist,AddOrder, deleteOrder} = props;

    if(isAccountOrderExist){
        return (<><button className="btn btn-primary" onClick={AddOrder}>update order</button> <button className="btn btn-primary" onClick={deleteOrder}>delete order</button></>);
    }
    return (<button className="btn btn-primary" onClick={AddOrder}>add order</button>);
}

export default ActionsBar;