import { createSlice } from "@reduxjs/toolkit";

export const OTPSlice = createSlice({
    name: 'otp',
    initialState: {
        otp: ''
    },
    reducers: {
        setOTP: (state, action) => {
            state.otp = action.payload
        }
    }
})

export const { setOTP } = OTPSlice.actions
export default OTPSlice.reducer