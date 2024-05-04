import { View, Text, FlatList } from "react-native";
import React from "react";
import { useSelector } from "react-redux";
import ItemPay from "./itemPay";
import Animated, { FadeIn, FadeOut } from "react-native-reanimated";

const ItemPayList = () => {
    const cart = useSelector((state) => state.cart.cart);

    return (
        <Animated.View entering={FadeIn} exiting={FadeOut}>
            <FlatList
                scrollEnabled={false}
                data={cart}
                renderItem={({ item }) => <ItemPay item={item} />}
                keyExtractor={(item) => item.MaSanPham}
            />
        </Animated.View>
    );
};

export default ItemPayList;
