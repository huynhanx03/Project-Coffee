import { View, Text, Image, TouchableOpacity, Pressable } from "react-native";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import React from "react";
import { useNavigation } from "@react-navigation/native";
import * as IconsSolid from "react-native-heroicons/solid";
import { colors } from "../theme";

const Item = () => {
    const navigation = useNavigation();
    return (
        <Pressable onPress={() => navigation.navigate('Detail')} className="bg-white rounded-[16px] space-y-2 mt-3">
            <View className="px-2">
                <View>
                    <Image
                        source={require("../assets/images/coffeeDemo.png")}
                        resizeMode="contain"
                        style={{ width: wp(40), height: wp(40) }}
                    />

                    <View className='absolute flex-row items-center bottom-[5px] right-0 p-1' style={{borderTopLeftRadius: 12, borderBottomRightRadius: 16, backgroundColor: 'rgba(0,0,0,0.16)'}}>
                        <IconsSolid.StarIcon size={16} color={'#fbbe21'} />
                        <Text className='text-white font-bold p-1'>5</Text>
                    </View>
                </View>
            </View>

            <View className="px-2 pb-2">
                <Text className="text-lg font-bold" numberOfLines={1} style={{ color: colors.text(1) }}>
                    Cà phê sữa đá
                </Text>
                <View className="flex-row justify-between items-center">
                    <Text className="text-sm font-semibold" style={{ color: colors.text(1) }}>
                        30.000đ
                    </Text>
                    <TouchableOpacity
                        className="items-center justify-center rounded-[10px]"
                        style={{ width: wp(8), height: wp(8), backgroundColor: colors.primary }}>
                        <Text className='text-lg text-white'>+</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </Pressable>
    );
};

export default Item;
