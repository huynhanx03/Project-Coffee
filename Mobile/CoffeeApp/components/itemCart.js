import { View, Text, Image, TouchableOpacity } from "react-native";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import React, { useEffect, useState } from "react";
import { colors } from "../theme";
import { useDispatch, useSelector } from "react-redux";
import { decrementQuantity, incrementQuantity, removeFromCart } from "../redux/slices/cartSlice";
import { deleteItemCard } from "../controller/CartController";
import * as Icons from "react-native-heroicons/outline";
import { formatPrice } from "../utils";

const ItemCart = (props) => {
    const dispatch = useDispatch();
    const cart = useSelector((state) => state.cart.cart)
    const [total, setTotal] = useState(0);

    const handleDeleteProduct = () => {
        dispatch(removeFromCart(props.item))
        deleteItemCard(props.item);
    }

    const handleIncrease = () => {
        dispatch(incrementQuantity(props.item))
    }

    const handleDecrese = () => {
        dispatch(decrementQuantity(props.item))
    }

    useEffect(() => {
        
    })
    return (
        <View className="mt-2">
            <View
                className="flex-row space-x-3 bg-white p-2 rounded-xl shadow-sm items-center">
                <Image source={{uri: props.item.HinhAnh}} style={{ width: wp(20), height: wp(22) }} className='rounded-lg'/>
                <View>
                    <Text className="text-lg font-semibold">{props.item.TenSanPham}</Text>
                    <View className="flex-row">
                        <Text className="italic text-gray-500 text-base">Size: </Text>
                        <Text className="text-gray-500 text-base font-semibold">{props.item.KichThuoc}</Text>
                    </View>
                    <View className="flex-row mt-2 justify-between items-end" style={{ width: wp(60) }}>
                        <View className="">
                            <Text className="text-red-500 text-xl font-semibold">{formatPrice(props.item.Gia)}</Text>
                        </View>

                        <View className="flex-row space-x-2">
                            <TouchableOpacity onPress={handleDecrese} className="p-2 rounded-lg items-center justify-center" style={{backgroundColor: colors.primary}}>
                                {/* <Text className='text-white'>-</Text> */}
                                <Icons.MinusIcon size={15} color='white' strokeWidth={4}/>
                            </TouchableOpacity>

                            <TouchableOpacity className="p-2 px-3 rounded-md border border-gray-200">
                                <Text className='font-semibold'>{props.item.SoLuongGioHang}</Text>
                            </TouchableOpacity>

                            <TouchableOpacity onPress={handleIncrease} className="p-2 rounded-lg items-center justify-center" style={{backgroundColor: colors.primary}}>
                                {/* <Text className='text-white'>+</Text> */}
                                <Icons.PlusIcon size={15} color='white' strokeWidth={4}/>
                            </TouchableOpacity>
                        </View>
                    </View>
                </View>

                <View className="absolute top-1 right-1">
                    <TouchableOpacity onPress={handleDeleteProduct} className='px-2 bg-gray-200 rounded-full'>
                        <Text className='text-base font-semibold'>x</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </View>
    );
};

export default ItemCart;
