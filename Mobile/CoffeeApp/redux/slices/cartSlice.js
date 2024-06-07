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
                itemPresent.SoLuong += action.payload.SoLuong;
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
                itemPresent.SoLuong++;
            }
        },
        decrementQuantity: (state, action) => {
            const itemPresent = state.cart.find(item => item.MaSanPham === action.payload.MaSanPham)

            if (itemPresent && itemPresent.SoLuong > 1) {
                itemPresent.SoLuong--;
            } else {
                state.cart = state.cart.filter(item => item.MaSanPham !== action.payload.MaSanPham)
            }
        },
        setQuantityInput: (state, action) => {
            if (action.payload.quantityInput < 1) {
                state.cart = state.cart.filter(item => item.MaSanPham !== action.payload.MaSanPham)
                return
            }

            const itemPresent = state.cart.find(item => item.MaSanPham === action.payload.MaSanPham)

            if (itemPresent) {
                itemPresent.SoLuong = action.payload.quantityInput;
            }
        },
        clearCart: (state) => {
            state.cart = []
        }
    }
})

export const { addToCart, addToCartFromDatabase, removeFromCart, incrementQuantity, decrementQuantity, setQuantityInput, clearCart } = CartSlice.actions
export default CartSlice.reducer