import { useState } from 'react';
import axios from 'axios';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import './App.css';


function App() {
    const [firstName, setFirstName] = useState("");
    const [firstNameError, setFirstNameError] = useState({ msg: "", err: false });
    const [lastName, setLastName] = useState("");
    const [lastNameError, setLastNameError] = useState({ msg: "", err: false });
    const [emailAddress, setEmailAddress] = useState("");
    const [emailAddressError, setEmailAddressError] = useState({ msg: "", err: false });
    const [phoneNumber, setPhoneNumber] = useState("");
    const [phoneNumberError, setPhoneNumberError] = useState({ msg: "", err: false });
    const alphabetsRegex = /^[a-zA-Z ]*$/;
    const emailRegex = /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/;
    const phoneRegex = /^!*([0-9]!*){10,}$/;

    const validateFirstName = (name: string) => {
        let isValid;

        if (name.trim() === "") {
            setFirstNameError({ msg: "This is required", err: true });
            isValid = false;
        } else if (!alphabetsRegex.test(name)) {
            setFirstNameError({ msg: "Enter alphabets only", err: true });
            isValid = false;
        } else if (name.trim().length < 2) {
            setFirstNameError({ msg: "Enter at least 2 characters", err: true });
            isValid = false;
        } else {
            setFirstNameError({ msg: "", err: false });
            isValid = true;
        }

        return isValid;
    }

    const validateLastName = (name: string) => {
        let isValid;

        if (name.trim() === "") {
            setLastNameError({ msg: "This is required", err: true });
            isValid = false;
        } else if (!alphabetsRegex.test(name)) {
            setLastNameError({ msg: "Enter alphabets only", err: true });
            isValid = false;
        } else if (name.trim().length < 2) {
            setLastNameError({ msg: "Enter at least 2 characters", err: true });
            isValid = false;
        } else {
            setLastNameError({ msg: "", err: false });
            isValid = true;
        }

        return isValid;
    }

    const validateEmailAddress = (email: string) => {
        let isValid;

        if (email.trim() === "") {
            setEmailAddressError({ msg: "This is required", err: true });
            isValid = false;
        } else if (!emailRegex.test(email)) {
            setEmailAddressError({ msg: "Enter valid email address", err: true });
            isValid = false;
        } else {
            setEmailAddressError({ msg: "", err: false });
            isValid = true;
        }

        return isValid;
    }

    const validatePhoneNumber = (phone: string) => {
        let isValid;

        if (phone === "") {
            setPhoneNumberError({ msg: "This is required", err: true });
            isValid = false;
        } else if (!phoneRegex.test(phone)) {
            setPhoneNumberError({ msg: "Enter at least 10 digits", err: true });
            isValid = false;
        } else {
            setPhoneNumberError({ msg: "", err: false });
            isValid = true;
        }

        return isValid;
    }

    const checkFormValidity = () => {
        const isFirstNameValid = validateFirstName(firstName);
        const isLastNameValid = validateLastName(lastName);
        const isEmailValid = validateEmailAddress(emailAddress);
        const isPhoneValid = validatePhoneNumber(phoneNumber);

        return isFirstNameValid && isLastNameValid && isEmailValid && isPhoneValid;
    }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.id === "firstName") {
            setFirstName(e.target.value);
            validateFirstName(e.target.value);
        } else if (e.target.id === "lastName") {
            setLastName(e.target.value);
            validateLastName(e.target.value);
        } else if (e.target.id === "emailAddress") {
            setEmailAddress(e.target.value);
            validateEmailAddress(e.target.value);
        } else if (e.target.id === "phoneNumber") {
            setPhoneNumber(e.target.value);
            validatePhoneNumber(e.target.value);
        }
    }

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const data = { firstName, lastName, emailAddress, phoneNumber };
        const formIsValid = checkFormValidity();

        if (formIsValid) {
            axios.post("https://localhost:7154/api/Users", data).then((res) => {
                console.log(res.status);

                setFirstName("");
                setLastName("");
                setEmailAddress("");
                setPhoneNumber("");

                alert("Successfully registered");
            })
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
                <TextField
                    id="firstName"
                    label="First Name"
                    variant="outlined"
                    required
                    fullWidth
                    value={firstName}
                    helperText={firstNameError.msg}
                    error={firstNameError.err}
                    onChange={handleChange}
                    onBlur={() => validateFirstName(firstName)}
                />
                <TextField
                    id="lastName"
                    label="Last Name"
                    variant="outlined"
                    required
                    fullWidth
                    value={lastName}
                    helperText={lastNameError.msg}
                    error={lastNameError.err}
                    onChange={handleChange}
                    onBlur={() => validateLastName(lastName)}
                />
                <TextField
                    id="emailAddress"
                    label="Email Address"
                    variant="outlined"
                    required
                    fullWidth
                    value={emailAddress}
                    helperText={emailAddressError.msg}
                    error={emailAddressError.err}
                    onChange={handleChange}
                    onBlur={() => validateEmailAddress(emailAddress)}
                />
                <TextField
                    id="phoneNumber"
                    label="Phone Number"
                    variant="outlined"
                    type="number"
                    required
                    fullWidth
                    value={phoneNumber}
                    helperText={phoneNumberError.msg}
                    error={phoneNumberError.err}
                    onChange={handleChange}
                    onBlur={() => validatePhoneNumber(phoneNumber)}
                />
                <Button variant="contained" type="submit">Submit</Button>
            </Box>
        </div>
    );
}

export default App;