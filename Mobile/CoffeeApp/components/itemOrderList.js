import { View, Text } from "react-native";
import React, { useEffect, useState } from "react";
import { getOrder } from "../controller/OrderController";
import ItemOrder from "./itemOrder";

const ItemOrderList = () => {
    const [orderInfo, setOrderInfo] = useState(null);

    const keyOrder = []

    const handleGetOrder = async () => {
        try {
            const info = await getOrder();
            setOrderInfo(info);
        } catch (error) {
            console.log(error);
        }
    };

    useEffect(() => {
        handleGetOrder();
    }, []);

    return (
        <View>
            {orderInfo &&
                Object.keys(orderInfo).reverse().map((key) => (
                    <ItemOrder key={key} order={orderInfo[key]} />
                ))}
        </View>
    );
};

export default ItemOrderList;
