import { useState } from 'react';
import axios from "axios"
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import './App.css';

function App() {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [emailAddress, setEmailAddress] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.id === "firstName") {
            setFirstName(e.target.value);
        } else if (e.target.id === "lastName") {
            setLastName(e.target.value);
        } else if (e.target.id === "emailAddress") {
            setEmailAddress(e.target.value);
        } else if (e.target.id === "phoneNumber") {
            setPhoneNumber(e.target.value);
        }
    }

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const data = { firstName, lastName, emailAddress, phoneNumber };

        if (firstName && lastName && emailAddress && phoneNumber) {
            console.log("DATA: ", data);

            axios.post("https://localhost:7154/api/Users", data).then((res) => {
                console.log("RESPONSE: ", res)

                setFirstName("")
                setLastName("")
                setEmailAddress("")
                setPhoneNumber("")
                alert("Successfully registered")
            })
        } else {
            alert("Please fill out all required fields")
        }
    }

    return (
        <div className="form">
            <h3>Register</h3>
            <Box
                component="form"
                sx={{
                    '& > :not(style)': { m: 1, width: '30vw' },
                    marginBottom: "1rem"
                }}
                noValidate
                autoComplete="off"
                onSubmit={handleSubmit}
            >
                <TextField id="firstName" label="First Name" variant="outlined" fullWidth required value={firstName} onChange={handleChange} />
                <TextField id="lastName" label="Last Name" variant="outlined" fullWidth required value={lastName} onChange={handleChange} />
                <TextField id="emailAddress" label="Email Address" variant="outlined" fullWidth required value={emailAddress} onChange={handleChange} />
                <TextField id="phoneNumber" label="Phone Number" variant="outlined" type="number" fullWidth required value={phoneNumber} onChange={handleChange} />
                <Button variant="contained" type="submit">Submit</Button>
            </Box>
        </div>
    );
}

export default App;