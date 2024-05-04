import { configureStore } from "@reduxjs/toolkit";
import CartReducer from "./slices/cartSlice";
import OTPReducer from "./slices/otpSlice";
import VoucherReducer from "./slices/voucherSlice";

export default configureStore({
    reducer: {
        cart: CartReducer,
        otp: OTPReducer,
        voucher: VoucherReducer,
    },
})