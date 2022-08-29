import React, { useState } from 'react';

const SelectComponenet = (props)=>{
    const selections = [
        {
            label: "Name",
            value: "name",
        },
        {
            label: "Haircut date",
            value: "bookingDate"
        }
    ];

    const [selection, setSelection] = useState("name");
    const handleChange = (e)=>{

        setSelection(e.target.value);
        props.sortSelect(e.target.value);
    };

    return (
            <select value={selection} onChange={handleChange}>
            {selections.map((option) =>(
                <option key={option.value} value={option.value}>{option.label}</option>
            ))}
            </select>
    );

} 

export default SelectComponenet;