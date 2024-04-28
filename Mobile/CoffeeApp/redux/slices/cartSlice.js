import { createSlice } from "@reduxjs/toolkit";

export const CartSlice = createSlice({
    name: 'cart',
    initialState: {
        cart: []
    },
    reducers: {
        addToCart: (state, action) => {
            console.log(action.payload)
            const itemPresent = state.cart.find(item => item.MaSanPham === action.payload.MaSanPham)

            if (itemPresent) {
                itemPresent.SoLuongGioHang += action.payload.SoLuongGioHang;
            } else {
                state.cart.push({ ...action.payload })
            }
        },
        addToCartFromDatabase: (state, action) => {
            state.cart.push({...action.payload})
        },
        removeFromCart: (state, action) => {
            state.cart = state.cart.filter(item => item.MaSanPham !== action.payload.MaSanPham)
        },
        incrementQuantity: (state, action) => {
            const itemPresent = state.cart.find(item => item.MaSanPham === action.payload.MaSanPham)

            if (itemPresent) {
                itemPresent.SoLuongGioHang++;
            }
        },
        decrementQuantity: (state, action) => {
            const itemPresent = state.cart.find(item => item.MaSanPham === action.payload.MaSanPham)

            if (itemPresent && itemPresent.SoLuongGioHang > 1) {
                itemPresent.SoLuongGioHang--;
            } else {
                state.cart = state.cart.filter(item => item.MaSanPham !== action.payload.MaSanPham)
            }
        },
        clearCart: (state) => {
            state.cart = []
        }
    }
})

export const { addToCart, addToCartFromDatabase, removeFromCart, incrementQuantity, decrementQuantity, clearCart } = CartSlice.actions
export default CartSlice.reducer