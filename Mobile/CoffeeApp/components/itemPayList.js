import { View, Text, FlatList } from "react-native";
import React from "react";
import { useSelector } from "react-redux";
import ItemPay from "./itemPay";

const ItemPayList = () => {
    const cart = useSelector((state) => state.cart.cart);

    return (
        <View>
            <FlatList
                scrollEnabled={false}
                data={cart}
                renderItem={({ item }) => <ItemPay item={item} />}
                keyExtractor={(item) => item.MaSanPham}
            />
        </View>
    );
};

export default ItemPayList;
