import { View, Text, Image, TouchableOpacity } from "react-native";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import React, { useEffect, useState } from "react";
import { colors } from "../theme";
import { useDispatch } from "react-redux";
import { removeFromCart } from "../redux/slices/cartSlice";

const ItemCart = (props) => {
    const dispatch = useDispatch();
    const [total, setTotal] = useState(0);

    const handleDeleteProduct = () => {
        dispatch(removeFromCart(props.item))
    }

    useEffect(() => {
        
    })
    return (
        <View className="mt-2">
            <View
                className="flex-row space-x-3 bg-white p-2 rounded-xl shadow-sm items-center">
                <Image source={{uri: props.item.HinhAnh}} style={{ width: wp(20), height: wp(20) }} />
                <View>
                    <Text className="text-lg font-semibold">{props.item.TenSanPham}</Text>
                    <View className="flex-row">
                        <Text className="italic text-gray-500 text-base">Size: </Text>
                        <Text className="text-gray-500 text-base font-semibold">{props.item.initialSize}</Text>
                    </View>
                    <View className="flex-row mt-2 justify-between items-center" style={{ width: wp(60) }}>
                        <View className="">
                            <Text className="text-red-500 text-base font-semibold">30.000đ</Text>
                        </View>

                        <View className="flex-row space-x-2">
                            <TouchableOpacity className="p-2 rounded-lg" style={{backgroundColor: colors.primary}}>
                                <Text className='text-white'>-</Text>
                            </TouchableOpacity>

                            <TouchableOpacity className="p-2 rounded-md border border-gray-200">
                                <Text>{props.item.quantity}</Text>
                            </TouchableOpacity>

                            <TouchableOpacity className="p-2 rounded-lg" style={{backgroundColor: colors.primary}}>
                                <Text className='text-white'>+</Text>
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
