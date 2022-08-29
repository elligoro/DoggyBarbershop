import React, { useState } from "react";
import { Link, Redirect,useHistory } from "react-router-dom";
import { useCookies, removeCookie } from "react-cookie";
import { signUp as signupCall } from "../../services/LoginService"; 
import { Status } from "../../services/Status";

const SignupPage = ()=>{
const emptySignup = {
                    firstName: "",
                    username: "",
                    password: "",
                    };

const [signup, setSignup] = useState(emptySignup);
const [signupErrors, setSignupErrors] = useState("");
const [cookies, setCookie] = useCookies(["access_token"]);
const errors = getErrors(signup);
const isValid = Object.keys(errors).length === 0;
const history = useHistory();

function handleSubmit(e){
    e.preventDefault();
    setSignupErrors("");

    if(!isValid)
        return;

    signupCall(signup)
            .then((data)=>{

            if(data.status == Status.ok)
            {
                setCookie('access_token', data.data.accessToken, { maxAge: data.data.expires * 1000, path: "/" });
                history.push(data.data.redirectTo);
                return;
            }
            if(data.status == Status.badRequest)
                throw "Please enter different username or password";
    
            if(data.status == Status.error)
                throw "Signup process has failed";

            })
            .catch(err => setSignupErrors(err))
}

function handleChange(e){
    setSignupErrors("");
    setSignup((currentSignup)=>{
        return {
            ...currentSignup, [e.target.id]: e.target.value
        };
    });
}

function getErrors(signup){
    const result = {};
    if(!signup.firstName) result.firstName = "First Name is required";
    if(!signup.username) result.username = "Username is required";
    if(!signup.password) result.password = "Password is required";

    return result;
}

    return(
    <div className="wrapper">
        <div className="log-header">
            <h1>
                Signup page
            </h1>
        </div>
        <div className="log-section">
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="firstName">First Name</label>
                    <input 
                        type="text"
                        id="firstName"
                        value={signup.firstName}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label htmlFor="username">Username</label>
                    <input 
                        type="text"
                        id="username"
                        value={signup.username}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label htmlFor="password">Password</label>
                    <input 
                        type="text"
                        id="password"
                        value={signup.password}
                        onChange={handleChange}
                    />
                </div>

                <div className="submit-actions">
                    <input 
                        type="submit"
                        className="btn btn-primary"
                        onClick={handleSubmit}
                        value="Submit"
                    />
                        <Link to="/login" className="btn btn-primary">
                            Login
                        </Link>
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

        {signupErrors && (
            <div role="alert" className="alerts">
                <p>Could not complete signup:</p>
                <p>{signupErrors}</p>
            </div>
        )}
    </div>
    </div>);
};

export default SignupPage;