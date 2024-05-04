import { createSlice } from "@reduxjs/toolkit";

export const VoucherSlice = createSlice({
    name: 'voucher',
    initialState: {
        voucher: {}
    },
    reducers: {
        setVoucher: (state, action) => {
            Object.assign(state.voucher, action.payload)
        },

        removeVoucher: (state) => {
            state.voucher = {}
        }
    }
})

export const { setVoucher, removeVoucher } = VoucherSlice.actions
export default VoucherSlice.reducer