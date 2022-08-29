import React, { Component, useState } from "react";
import { Redirect, Route, Switch } from "react-router-dom";
import PropTypes from "prop-types";
import { cookies, useCookies } from 'react-cookie';

import LoginPage from "./login/LoginPage";
import SignupPage from "./login/SignupPage";
import OrdersPage from "./orders/OrdersPage";
import Header from "./common/Header";

// https://stackoverflow.com/questions/48497510/simple-conditional-routing-in-reactjs
// https://jasonwatmore.com/post/2021/09/09/react-redirect-to-login-page-if-unauthenticated

function PrivateRoutes({ component: Component, ...rest }){
    const [cookies, setCookie] = useCookies(['access-token']);
    return (
        <Route {...rest} render={props => {
            if(!cookies.access_token) {
                return <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
            }

            return <Component {...props} cookie={cookies} />
        }} />
    );
}    

function App(){
    return (
        <div className="container-fluid">
            <Header />
            <Switch>
                <PrivateRoutes exact path='/' component={OrdersPage} />
                <Route path="/login" component={LoginPage} />
                <Route path="/signup" component={SignupPage} />
                <Redirect from='*' to='/' />
            </Switch>
        </div>
    )
}

export default App;