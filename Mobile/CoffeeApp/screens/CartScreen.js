import { useFocusEffect, useNavigation } from "@react-navigation/native";
import React, { useCallback, useEffect, useState } from "react";
import {
    FlatList,
    Pressable,
    ScrollView,
    Text,
    TouchableOpacity,
    View,
    Alert,
} from "react-native";
import * as Icons from "react-native-heroicons/outline";
import { Divider } from "react-native-paper";
import { widthPercentageToDP as wp } from "react-native-responsive-screen";
import { SafeAreaView } from "react-native-safe-area-context";
import ItemCart from "../components/itemCart";
import { getAddress } from "../controller/AddressController";
import getDefaultAddress from "../customHooks/getDefaultAddress";
import { useDispatch, useSelector } from "react-redux";
import { clearCart } from "../redux/slices/cartSlice";
import { removeItemCart } from "../controller/CartController";
import { formatPrice } from "../utils";

const CartScreen = () => {
    const navigation = useNavigation();
    const dispatch = useDispatch();
    const cart = useSelector((state) => state.cart.cart);
    const addressData = getDefaultAddress();
    const [totalPrice, setTotalPrice] = useState(0);

    //delete all items in cart (redux and database)
    const handleDeleteCart = () => {
        Alert.alert(
            "Xác nhận",
            "Bạn có chắc chắn xoá hết sản phẩm?",
            [
                { text: "Hủy", style: "cancel" },
                { text: "Xoá", style: 'destructive', onPress: () => {
                    dispatch(clearCart());
                    removeItemCart();
                } },
            ],
            { cancelable: true }
        );
    }

    //calculate total price of all items in cart
    const handleTotalPrice = () => {
        let total = 0;
        cart.forEach((item) => {
            total += item.SoLuongGioHang * item.Gia;
        });

        setTotalPrice(total);
    }

    useEffect(() => {
        handleTotalPrice();
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
                // showsVerticalScrollIndicator={false}
                >
                {/* address */}
                <View className="flex-row items-center gap-3">
                    <Icons.MapPinIcon size={24} color={"red"} />
                    <Text className="text-base">Địa chỉ nhận hàng</Text>
                </View>

                <Pressable
                    onPress={() => navigation.navigate("Address")}
                    className="ml-9 space-y-1 flex-row justify-between">
                    <View style={{ width: wp(70) }}>
                        <View className="flex-row gap-1 items-center">
                            <Text className="text-base">
                                {addressData?.HoTen}
                            </Text>
                            <Text>|</Text>
                            <Text className="text-base">
                                {addressData?.SoDienThoai}
                            </Text>
                        </View>
                        <View>
                            <Text>{addressData?.DiaChi}</Text>
                        </View>
                    </View>

                    <View>
                        <Icons.ChevronRightIcon size={24} color={"black"} />
                    </View>
                </Pressable>

                <Divider />

                {/* item cart */}
                <View className="space-y-2 flex-1">
                    <FlatList 
                        data={cart}
                        keyExtractor={(item) => item.MaSanPham}
                        renderItem={({item}) => <ItemCart item={item} />}
                    />
                </View>
            </View>

            <View className="absolute bottom-5 w-full">
                <TouchableOpacity className="flex-row justify-between items-center mx-5 bg-amber-500 rounded-full p-5">
                    <Text className="text-lg font-semibold">Thanh toán</Text>
                    <Text className="text-lg font-semibold">{formatPrice(totalPrice)}</Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

export default CartScreen;
