import { View, Text, Image, TouchableOpacity } from "react-native";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import React from "react";
import { colors } from "../theme";

const ItemCart = () => {
    return (
        <View className="mt-2">
            <View
                className="flex-row space-x-3 bg-white p-2 rounded-xl shadow-sm items-center">
                <Image source={require("../assets/images/coffeeDemo.png")} style={{ width: wp(20), height: wp(20) }} />
                <View>
                    <Text className="text-lg font-semibold">Espresso</Text>
                    <View className="flex-row">
                        <Text className="italic text-gray-500 text-base">Size: </Text>
                        <Text className="text-gray-500 text-base font-semibold">L</Text>
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
                                <Text>1</Text>
                            </TouchableOpacity>

                            <TouchableOpacity className="p-2 rounded-lg" style={{backgroundColor: colors.primary}}>
                                <Text className='text-white'>+</Text>
                            </TouchableOpacity>
                        </View>
                    </View>
                </View>

                <View className="absolute top-1 right-1">
                    <TouchableOpacity className='px-2 bg-gray-200 rounded-full'>
                        <Text className='text-base font-semibold'>x</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </View>
    );
};

export default ItemCart;
