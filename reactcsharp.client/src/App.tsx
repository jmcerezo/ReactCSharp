import {  useState } from 'react';
import axios from 'axios';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import './App.css';


function App() {
    const [firstName, setFirstName] = useState("");
    const [firstNameError, setFirstNameError] = useState("");
    const [lastName, setLastName] = useState("");
    const [lastNameError, setLastNameError] = useState("");
    const [emailAddress, setEmailAddress] = useState("");
    const [emailAddressError, setEmailAddressError] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [phoneNumberError, setPhoneNumberError] = useState("");
    const alphabetsRegex = /^[a-zA-Z ]*$/
    const emailRegex = /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/
    const phoneRegex = /^!*([0-9]!*){10,}$/

    const validateFirstName = (name: string) => {
        let isValid;

        if (name.trim() === "") {
            setFirstNameError("This is required");
            isValid = false;
        } else if (!alphabetsRegex.test(name)) {
            setFirstNameError("Enter alphabets only")
            isValid = false;
        } else if (name.trim().length < 2) {
            setFirstNameError("Enter at least 2 characters")
            isValid = false;
        } else {
            setFirstNameError("")
            isValid = true;
        }

        return isValid
    }

    const validateLastName = (name: string) => {
        let isValid;

        if (name.trim() === "") {
            setLastNameError("This is required");
            isValid = false;
        } else if (!alphabetsRegex.test(name)) {
            setLastNameError("Enter alphabets only")
            isValid = false;
        } else if (name.trim().length < 2) {
            setLastNameError("Enter at least 2 characters")
            isValid = false;
        } else {
            setLastNameError("")
            isValid = true;
        }

        return isValid
    }

    const validateEmailAddress = (email: string) => {
        let isValid;

        if (email.trim() === "") {
            setEmailAddressError("This is required");
            isValid = false;
        } else if (!emailRegex.test(email)) {
            setEmailAddressError("Enter valid email address")
            isValid = false;
        } else {
            setEmailAddressError("")
            isValid = true;
        }

        return isValid
    }

    const validatePhoneNumber = (phone: string) => {
        let isValid;

        if (!phone) {
            setPhoneNumberError("This is required");
            isValid = false;
        } else if (!phoneRegex.test(phone)) {
            setPhoneNumberError("Enter at least 10 digits")
            isValid = false;
        } else {
            setPhoneNumberError("")
            isValid = true;
        }

        return isValid
    }

    const validateForm = () => {
        validateFirstName(firstName)
        validateLastName(lastName)
        validateEmailAddress(emailAddress)
        validatePhoneNumber(phoneNumber)

        return (
            validateFirstName(firstName) &&
            validateLastName(lastName) &&
            validateEmailAddress(emailAddress) &&
            validatePhoneNumber(phoneNumber)
        )
    }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.id === "firstName") {
            setFirstName(e.target.value)
            validateFirstName(e.target.value)
        } else if (e.target.id === "lastName") {
            setLastName(e.target.value)
            validateLastName(e.target.value)
        } else if (e.target.id === "emailAddress") {
            setEmailAddress(e.target.value)
            validateEmailAddress(e.target.value)
        } else if (e.target.id === "phoneNumber") {
            setPhoneNumber(e.target.value)
            validatePhoneNumber(e.target.value)
        }
    }

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const data = { firstName, lastName, emailAddress, phoneNumber };
        const formIsValid = validateForm()

        if (formIsValid) {
            axios.post("https://localhost:7154/api/Users", data).then((res) => {
                console.log(res.status)

                setFirstName("")
                setLastName("")
                setEmailAddress("")
                setPhoneNumber("")

                alert("Successfully registered")
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
                <TextField id="firstName" label="First Name" variant="outlined" required fullWidth value={firstName} helperText={firstNameError} onChange={handleChange} />
                <TextField id="lastName" label="Last Name" variant="outlined" required fullWidth value={lastName} helperText={lastNameError} onChange={handleChange} />
                <TextField id="emailAddress" label="Email Address" variant="outlined" required fullWidth value={emailAddress} helperText={emailAddressError} onChange={handleChange} />
                <TextField id="phoneNumber" label="Phone Number" variant="outlined" type="number" required fullWidth value={phoneNumber} helperText={phoneNumberError} onChange={handleChange} />
                <Button variant="contained" type="submit">Submit</Button>
            </Box>
        </div>
    );
}

export default App;