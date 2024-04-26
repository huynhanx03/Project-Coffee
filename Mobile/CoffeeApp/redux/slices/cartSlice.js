import { createSlice } from "@reduxjs/toolkit";

export const CartSlice = createSlice({
    name: 'cart',
    initialState: {
        cart: []
    },
    reducers: {
        addToCart: (state, action) => {
            const itemPresent = state.cart.find(item => item.MaSanPham === action.payload.MaSanPham)

            if (itemPresent) {
                itemPresent.quantity += action.payload.quantity;
            } else {
                state.cart.push({ ...action.payload })
            }
        },
        removeFromCart: (state, action) => {
            state.cart = state.cart.filter(item => item.MaSanPham !== action.payload.MaSanPham)
        },
        incrementQuantity: (state, action) => {
            const itemPresent = state.cart.find(item => item.MaSanPham === action.payload.MaSanPham)

            if (itemPresent) {
                itemPresent.quantity++;
            }
        },
        decrementQuantity: (state, action) => {
            const itemPresent = state.cart.find(item => item.MaSanPham === action.payload.MaSanPham)

            if (itemPresent && itemPresent.quantity > 1) {
                itemPresent.quantity--;
            } else {
                state.cart = state.cart.filter(item => item.MaSanPham !== action.payload.MaSanPham)
            }
        },
        clearCart: (state) => {
            state.cart = []
        }
    }
})

export const { addToCart, removeFromCart, incrementQuantity, decrementQuantity, clearCart } = CartSlice.actions
export default CartSlice.reducer