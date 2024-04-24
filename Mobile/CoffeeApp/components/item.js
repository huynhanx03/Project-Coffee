import { View, Text, Image, TouchableOpacity, Pressable, Touchable } from "react-native";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import React, { useEffect, useState } from "react";
import { useNavigation } from "@react-navigation/native";
import * as IconsSolid from "react-native-heroicons/solid";
import { colors } from "../theme";
import { formatPrice } from "../utils";

const Item = ({product}) => {
    const initialPrice = formatPrice(product.Size.Thuong.Gia);
    const navigation = useNavigation();
    const [size, setSize] = useState('M')
    const [price, setPrice] = useState(initialPrice);

    const handleSizeAndPrice = (size) => {
        setSize(size);
        if (size === 'S') {
            setPrice(formatPrice(product.Size.Nho.Gia))
        } else if (size === 'M') {
            setPrice(formatPrice(product.Size.Thuong.Gia))
        } else {
            setPrice(formatPrice(product.Size.Lon.Gia))
        }
    }
    return (
        <Pressable onPress={() => navigation.navigate('Detail', {...product})} className="bg-white rounded-[16px] space-y-2 mt-3">
            <View className="px-1">
                <View className='p-1 justify-center items-center mt-1' style={{width: wp(40), height: wp(40)}}>
                    <Image
                        source={{uri: product.HinhAnh}}
                        resizeMode="contain"
                        style={{ width: wp(40), height: wp(40) }}
                        className='rounded-[16px]'
                    />

                    <View className='absolute flex-row items-center bottom-0 right-0 p-1' style={{borderTopLeftRadius: 12, borderBottomRightRadius: 16, backgroundColor: 'rgba(0,0,0,0.16)'}}>
                        <IconsSolid.StarIcon size={16} color={'#fbbe21'} />
                        <Text className='text-white font-bold p-1'>5</Text>
                    </View>
                </View>
            </View>

            <View className="px-2 pb-2">
                <Text className="text-lg font-bold" numberOfLines={1} style={{ color: colors.text(1) }}>
                    {product.TenSanPham}
                </Text>
                <View className="flex-row justify-between items-center">
                    <Text className="text-sm font-semibold" style={{ color: colors.text(1) }}>
                        {price}
                    </Text>
                    
                    <View className='flex-row gap-2'>
                        <TouchableOpacity onPress={() => handleSizeAndPrice('S')} className={'p-1 px-2 rounded-md ' + (size === 'S' ? 'bg-amber-600' : 'bg-amber-200')}>
                            <Text>S</Text>
                        </TouchableOpacity>
                        <TouchableOpacity onPress={() => handleSizeAndPrice('M')} className={'p-1 px-2 rounded-md ' + (size === 'M' ? 'bg-amber-600' : 'bg-amber-200')}>
                            <Text>M</Text>
                        </TouchableOpacity>
                        <TouchableOpacity onPress={() => handleSizeAndPrice('L')} className={'p-1 px-2 rounded-md ' + (size === 'L' ? 'bg-amber-600' : 'bg-amber-200')}>
                            <Text>L</Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </View>
        </Pressable>
    );
};

export default Item;
