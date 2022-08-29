import React from "react";
import { render } from "react-dom";
import { BrowserRouter as Router } from "react-router-dom";
import { CookiesProvider } from 'react-cookie';
import "bootstrap/dist/css/bootstrap.min.css";
import App from "./components/App";

render(
    <CookiesProvider>
        <Router>
            <App />
        </Router>
    </CookiesProvider>,
    document.getElementById("app")
);