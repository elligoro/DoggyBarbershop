//import { useState } from "react";
//import { useCookies, removeCookie } from "react-cookie";
import { Status } from "./Status";
const baseUrl = "http://localhost:14445";

export async function login(loginModel){
    //const [cookies, setCookie] = useCookies(["access_token"]);
        var body = JSON.stringify(loginModel);

        var res = await fetch(baseUrl + "/login", {
                                        method: 'POST',
                                        mode: 'cors',
                                        headers: {
                                            'Content-Type': 'application/json',
                                        },
                                        body: body
                                    });
        return res.json();
}  
export async function signUp(signupModel){

    const res = await fetch(baseUrl + "/signup",{
        method: 'POST',
        mode:'cors',
        headers: {
        'Content-Type': 'application/json',
        },
        body: JSON.stringify(signupModel)
    });
    
    return res.json();
}  

/*
export async function logout(){
    try{
        const res = await fetch(baseUrl + "/logout");
        
        // return redirect to login page componenet url
        if(res.ok){
            setCookie('access_token', {expires: new Date()});
            return res.data.redirectTo;
        }
    }catch(err){
        throw "Unknown issue has occurred";
    }  
} 
*/