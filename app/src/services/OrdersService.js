import React, { useState, useEffect } from "react";
import { useCookies } from 'react-cookie';

const baseUrl = "http://localhost:14445/";
const url = baseUrl + "order/";

export async function getOrders(cookie){ 
            const orders = await fetch(baseUrl + "orders/", {
                                                            method: 'GET',
                                                            headers: {
                                                            'Content-Type': 'application/json',
                                                            'Authorization' : 'Bearer '+ cookie
                                                            },
                                                            mode: 'cors',                                                         
                                                        })
                                    .then((res) => res.json())
                                    .then((data) => {
                                        return data;
                                    });
            console.log(orders);
            return orders;
}

export async function addOrder(order,cookie){
        try{
            const orders = await fetch(url,        {
                                        method: 'POST',
                                        headers: {
                                        'Content-Type': 'application/json',
                                        'Authorization' : 'Bearer '+ cookie
                                        },
                                        mode: 'cors',
                                        body: JSON.stringify(order),
                                    })
                                    .then((res) => res.json())
                                    .then((data) => {
                                        return data;
                                    }); 
            return orders;
        }catch(err){
            console.log(err); 
        }
}

export async function updateOrder(orderId,order){
        //const [cookies,setCookie] = useCookies(['access-token']);
        try{
            await fetch(url + orderId, {
                                method: 'PUT',
                                headers: {
                                'Content-Type': 'application/json',
                                /*'Authorization' : 'Bearer '+ cookies.get("access-token"),*/
                                },
                                body: JSON.stringify(order),
                            });
        }catch(err){
            console.log(err); 
        }
    }
export async function deleteOrder(orderId,cookie){
        try{
            const orders = await fetch(url + orderId, {
                                    method: 'DELETE',
                                    headers: {
                                        'Content-Type': 'application/json',
                                        'Authorization' : 'Bearer '+ cookie,
                                        }
                                })
                                .then((res) => res.json())
                                .then((data) => {
                                    return data;
                                }); 
        return orders;
        }catch(err){
            console.log(err); 
        }
}
