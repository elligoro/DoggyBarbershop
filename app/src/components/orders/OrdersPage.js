import React, { Component } /*{ useState, useEffect }*/ from "react";
import { Link } from 'react-router-dom'; 
import { render } from "react-dom";
import PropTypes from "prop-types";
import OrdersList from "./components/OrdersList";
import * as ordersApi from "../../services/OrdersService"; 
import ModalPopUp from "../common/ModalPopUp";
import OrderComponent from "./components/OrderComponent";
import ReadDetailsComponent from "./components/ReadDetailsComponent";
import ActionsBar from "../common/ActionsBar";
import SelectComponenet from "../common/SelectComponenet";

class OrdersPage extends Component {

    isOrdersExist = this.orders?.length > 0;
    accountOrder = this.isOrdersExist && (OrdersList.find(o => o.accountId == this.accountId));
    emptyOrder = { bookingDate: new Date(), orderId: null };

    constructor(props){
        super();
        this.state = { orders: [],
                       orderUpdate: this.emptyOrder,
                       accountId: null,
                       accountOrder: null,
                       isOpenModal: false,
                       isAccountOrderExist: false,
                       isReadOnly: false, 
                       readonlyOrder: null
                    };
        this.getOrders = this.getOrders.bind(this);
        this.submitOrder = this.submitOrder.bind(this);
        this.getSingleOrderData = this.getSingleOrderData.bind(this);
        this.getOrderForUpdate = this.getOrderForUpdate.bind(this);
        this.deleteOrder = this.deleteOrder.bind(this);
        this.showDetails = this.showDetails.bind(this);
        this.sortSelect = this.sortSelect.bind(this);

        this.cookie = props.cookie?.access_token;
    }

    async componentDidMount(){
        await this.getOrders();
        let accountId = this.state.accountId
        let order = this.state.orders.find(o=>o.accountId == accountId);
        this.setState(prevstate => ({   ...prevstate,
                                        accountId: accountId,
                                        accountOrder: order ?? null,
                                        isAccountOrderExist: !!order
                                    }));
        
    }


    async getOrders(){
        let { orders, accountId } = await ordersApi.getOrders(this.cookie);
        let accountOrder = this.getSingleOrderData();
        this.setState(prevstate => ({ ...prevstate,
                                      orders: orders, 
                                      accountOrder:accountOrder,
                                      isAccountOrderExist: !!this.getSingleOrderData(), 
                                      accountId: accountId }));

        this.sortSelect(this.state.sortSelect);
    }

    async deleteOrder(){
        let accountOrder = this.getSingleOrderData();
        this.setState(prevstate => ({ ...prevstate,
                                        accountOrder: accountOrder, 
                                        isAccountOrderExist: !!accountOrder }));
       
       if(!this.state.accountOrder)
            return;

       let {orders} = await ordersApi.deleteOrder(this.state.accountOrder.orderId,this.cookie);
       this.setState(prevstate => ({ ...prevstate,
                                    orders: orders.sort((o1,o2) => o1[prevstate.sortSelect] - o2[prevstate.sortSelect]), 
                                    isAccountOrderExist: !!this.getSingleOrderData() }));

        this.sortSelect(this.state.sortSelect);
    }
    
    openModal = ()=>{
        this.setState(prevstate => ({...prevstate, isOpenModal: true }));
    };

    async submitOrder(updatedOrder){

        let { orders } = await ordersApi.addOrder(updatedOrder,this.cookie);
        
        this.setState(prevstate => ({ ...prevstate,
                                      isOpenModal: !prevstate.isOpenModal,
                                      orderUpdate: this.emptyOrder,
                                      orders: orders.sort((o1,o2) => o1[prevstate.sortSelect] - o2[prevstate.sortSelect]), 
                                      accountOrder: this.getSingleOrderData(),
                                      isAccountOrderExist: !!this.getSingleOrderData(),
                                       }));
        
        this.sortSelect(this.state.sortSelect);
    }

    showDetails(orderId){
        let order = this.state.orders.find(o => o.orderId == orderId);
        this.setState(prevstate => ({ ...prevstate, isReadOnly: true, isOpenModal: true, readonlyOrder: order }));
    }

    getSingleOrderData(){
        let accountId = this.state.accountId
        return this.state.orders.find(o=>o.accountId == accountId);
    }

    getOrderForUpdate(){
        return this.getSingleOrderData() ?? this.emptyOrder;
    }

    closeOrder = ()=>{
        this.setState(prevstate => ({...prevstate, isOpenModal: false, isReadOnly: false }));
    };

    getSortCallback(sortType){
        return (sortType == "name") ? ((a,b)=>{ 
                                if(a[sortType] < b[sortType]) return -1
                                if(a[sortType] > b[sortType]) return 1;
                                return 0; 
                                }) : ((a,b)=> new Date(a[sortType]) - new Date(b[sortType]));
    }

    sortSelect(selectionType){
        let predicate = this.getSortCallback(selectionType);
        let orders = [...this.state.orders]?.sort(predicate);
        this.setState(prevstate => ({...prevstate, 
                                        sortSelect: selectionType,
                                        orders: orders, 
                                    }));
    }

    render(){
        const { orders, accountId, isAccountOrderExist, isReadOnly, readonlyOrder } = this.state;
        return (
            <>
                <ModalPopUp isOpenModal={this.state.isOpenModal} 
                                        isReadOnly={isReadOnly}
                                        component={isReadOnly ? ReadDetailsComponent : OrderComponent} 
                                        order={isReadOnly ? readonlyOrder : this.getOrderForUpdate}
                                        onSubmit={this.submitOrder} 
                                        onClose={this.closeOrder} 
                                        containerId="#app"/>
                <div>
                    <Link to="/signup" className="btn btn-primary btn-lg">
                        Sign up
                    </Link>

                    <h2>Dog haircut schedule</h2>
                    <div>
                        <h4>Sort By:</h4>
                        <SelectComponenet sortSelect={this.sortSelect}/>

                    </div>
                    <OrdersList orders={orders} 
                                accountId={accountId} 
                                isAccountOrderExist = {orders?.find(o => o.accountId == accountId)} 
                                openModal = {this.openModal} 
                                deleteOrder = {this.deleteOrder} 
                                showDetails = {this.showDetails}
                                />
                </div>
            </>
        );
    }
}

export default OrdersPage;