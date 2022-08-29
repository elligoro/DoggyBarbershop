import React, { useState } from "react";
import { Link, Redirect, useHistory } from "react-router-dom";
import { useCookies } from "react-cookie";
import { Status } from "../../services/Status";

import { login as loginCall }  from "../../services/LoginService"; 

function LoginPage(){
    const emptyLogin = {
                        username: "",
                        password: ""
                        };

    const [login, setLogin] = useState(emptyLogin);
    const [loginErrors, setLoginErrors] = useState("");
    const [cookies, setCookie] = useCookies(["access_token"]);
    const errors = getErrors(login);
    const isValid = Object.keys(errors).length === 0;
    const history = useHistory();

    function handleSubmit(e){
        e.preventDefault();
        setLoginErrors("");

        if(isValid)
        {
            loginCall(login)
            .then((data) => {
                
            if(data.status == Status.ok){
                setCookie('access_token', data.data.accessToken, { maxAge: data.data.expires * 1000, path: "/" });
                history.push(data.data.redirectTo);
                return;
            }
    
            if(data.status == Status.badRequest)
                throw "User or password do not match";
    
            if(data.status == Status.error)
                throw "Login process has failed";

            })
            .catch((err)=> setLoginErrors(err));
        }
    }

    function handleChange(e){
        setLoginErrors("");
        setLogin((currentLogin)=>{
            return {
                ...currentLogin, [e.target.id]: e.target.value
            };
        });
    }

    function getErrors(login){
        const result = {};
        if(!login.username) result.username = "Username is required";
        if(!login.password) result.password = "Password is required";
        return result;
    }

    return (
        <div className="wrapper">
            <h1>
                login page
            </h1>
            <Link to="/signup" className="btn btn-primary btn-lg">
                Sign up
            </Link>
            <div className="log-section">
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="username">Username</label>
                    <input 
                        type="text"
                        id="username"
                        value={login.username}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label htmlFor="password">Password</label>
                    <input 
                        type="text"
                        id="password"
                        value={login.password}
                        onChange={handleChange}
                    />
                </div>

                <div>
                    <input 
                        type="submit"
                        className="btn btn-primary"
                        onClick={handleSubmit}
                        value="Submit"
                    />
                </div>
            </form>
            {!isValid && (
            <div role="alert" className="alerts">
                <p>Not all the fields are valid:</p>
                <ul>
                    {Object.keys(errors).map(key => {
                        return (<li key={key}>{errors[key]}</li>);
                    })}
                </ul>
            </div>
        )}
        {loginErrors && (
            <div role="alert" className="alerts">
                <p>Could not complete signup:</p>
                <p>{loginErrors}</p>
            </div>
        )}
        </div>
        </div>
    );
}
export default LoginPage;