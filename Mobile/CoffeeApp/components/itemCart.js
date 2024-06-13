import { View, Text, TouchableOpacity, TextInput } from "react-native";
import { Image } from "expo-image";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import React, { useEffect, useState } from "react";
import { colors } from "../theme";
import { useDispatch } from "react-redux";
import { decrementQuantity, incrementQuantity, removeFromCart, setQuantityInput } from "../redux/slices/cartSlice";
import { deleteItemCard } from "../controller/CartController";
import * as Icons from "react-native-heroicons/outline";
import { formatPrice } from "../utils";
import ShowToast from "./toast";
import { getProductDetailById } from "../controller/ProductController";
import { blurhash } from "../utils";
import Animated, { ZoomIn } from "react-native-reanimated";

const ItemCart = (props) => {
    const dispatch = useDispatch();
    const [quantity, setQuantity] = useState(props.item.SoLuong);
    const [available, setAvailable] = useState(0);

    const handleDeleteProduct = () => {
        dispatch(removeFromCart(props.item))
        deleteItemCard(props.item);
    }

    const getQuantityAvailable = async () => {
        try {
            const product = await getProductDetailById(props.item.MaSanPham);
            setAvailable(product.SoLuong);
        } catch (err) {
            console.log(err);
        }
    }

    useEffect(() => {
        getQuantityAvailable();
    }, [])
    const handleIncrease = () => {
        if (quantity < available) {
            setQuantity((quantity) => quantity + 1);
            dispatch(incrementQuantity(props.item))
        } else {
            ShowToast('error', 'Lỗi', 'Số lượng sản phẩm không đủ!')
        }
    }

    const handleDecrese = () => {
        setQuantity((quantity) => quantity - 1)
        dispatch(decrementQuantity(props.item))
    }

    const changeQuantityInput = () => {
        if (quantity > available) {
            ShowToast('error', 'Lỗi', 'Số lượng sản phẩm không đủ!')
            setQuantity(available)
            return;
        }
        if (quantity < 1) {
            deleteItemCard(props.item);
        }

        dispatch(setQuantityInput({...props.item, quantityInput: +quantity}))

    }

    return (
        <Animated.View entering={ZoomIn.duration(600)} className="mt-2">
            <View
                className="flex-row space-x-3 bg-white p-2 rounded-xl shadow-sm items-center">
                <Image source={{uri: props.item.HinhAnh}} style={{ width: wp(20), height: wp(22) }} contentFit="contain" className='rounded-lg' placeholder={{ blurhash }} transition={1000}/>
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
                                <Icons.MinusIcon size={15} color='white' strokeWidth={4}/>
                            </TouchableOpacity>

                            <View className="rounded-md border border-gray-200">
                                <TextInput onBlur={changeQuantityInput} className='p-2 px-3' keyboardType="number-pad" value={quantity.toString()} onChangeText={e => setQuantity(+e)} />
                            </View>

                            <TouchableOpacity onPress={handleIncrease} className="p-2 rounded-lg items-center justify-center" style={{backgroundColor: colors.primary}}>
                                <Icons.PlusIcon size={15} color='white' strokeWidth={4}/>
                            </TouchableOpacity>
                        </View>
                    </View>
                </View>

                <View className="absolute top-1 right-1">
                    <TouchableOpacity onPress={handleDeleteProduct} className='p-1 bg-gray-200 rounded-full'>
                        <Icons.XMarkIcon size={20} color='black' strokeWidth={2}/>
                    </TouchableOpacity>
                </View>
            </View>
        </Animated.View>
    );
};

export default ItemCart;
