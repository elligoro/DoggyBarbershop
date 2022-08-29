import React from "react";
import { NavLink } from "react-router-dom";
import logo from '../../images/logo.png';

const Header = () => {
    const activeStyle = { color: "#F15B2A"};

    return (
        <nav>
            <NavLink to="/">
                <img src={logo} />
           </NavLink>
        </nav>
    );
};

export default Header;