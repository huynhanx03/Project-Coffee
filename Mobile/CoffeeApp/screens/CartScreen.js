import { useNavigation } from "@react-navigation/native";
import React, { useMemo, useState } from "react";
import {
    FlatList,
    Text,
    TouchableOpacity,
    View,
} from "react-native";
import * as Icons from "react-native-heroicons/outline";
import { widthPercentageToDP as wp } from "react-native-responsive-screen";
import { SafeAreaView } from "react-native-safe-area-context";
import ItemCart from "../components/itemCart";
import { useDispatch, useSelector } from "react-redux";
import { clearCart } from "../redux/slices/cartSlice";
import { removeItemCart } from "../controller/CartController";
import { formatPrice } from "../utils";
import { useNotification } from "../context/ModalContext";

const CartScreen = () => {
    const navigation = useNavigation();
    const dispatch = useDispatch();
    const cart = useSelector((state) => state.cart.cart);
    const [totalPrice, setTotalPrice] = useState(0);
    const { showNotification } = useNotification();

    //delete all items in cart (redux and database)
    const removeAll = () => {
        dispatch(clearCart());
        removeItemCart();
    }
    const handleDeleteCart = () => {
        if (cart.length === 0) {
            return
        }
        showNotification("Bạn có muốn xoá hết đơn hàng?", "inform", removeAll);
    }

    //calculate total price of all items in cart
    const handleTotalPrice = useMemo(() => {
        let total = 0;
        cart.forEach((item) => {
            total += item.SoLuong * item.Gia;
        });

        setTotalPrice(total);
    }, [cart])

    return (
        <View className="flex-1">
            <SafeAreaView
                style={{
                    backgroundColor: "#f2f2f2",
                    shadowColor: "#000000",
                    shadowOffset: { width: 0, height: 4 },
                    shadowOpacity: 0.19,
                    shadowRadius: 5.62,
                    elevation: 6,
                }}>
                {/* header */}
                <View className="flex-row justify-between items-center mx-5">
                    <TouchableOpacity onPress={() => navigation.goBack()}>
                        <Icons.ChevronLeftIcon size={30} color={"black"} />
                    </TouchableOpacity>
                    <Text className="text-lg font-semibold tracking-wider">
                        Giỏ hàng
                    </Text>
                    <TouchableOpacity onPress={handleDeleteCart}>
                        <Icons.TrashIcon size={30} color={"black"} />
                    </TouchableOpacity>
                </View>
            </SafeAreaView>

            <View
                className="mx-5 pt-2 space-y-3 flex-1"
            >

                {/* item cart */}
                <View className="space-y-2 flex-1">
                    <FlatList 
                        data={cart}
                        keyExtractor={(item) => item.MaSanPham}
                        renderItem={({item}) => <ItemCart item={item} />}
                    />
                </View>
            </View>

            {/* change to prepare pay screen */}
            <View className="absolute bottom-5 w-full">
                <TouchableOpacity onPress={() => navigation.navigate('Prepare')} className="flex-row justify-between items-center mx-5 bg-amber-500 rounded-full p-5">
                    <Text className="text-lg font-semibold">Thanh toán</Text>
                    <Text className="text-lg font-semibold">{formatPrice(totalPrice)}</Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

export default CartScreen;
