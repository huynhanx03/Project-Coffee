import { configureStore } from "@reduxjs/toolkit";
// import CartReducer from "./slices/cartSlice";
import OTPReducer from "./slices/otpSlice";

export default configureStore({
    reducer: {
        // cart: CartReducer,
        otp: OTPReducer,
    },
})