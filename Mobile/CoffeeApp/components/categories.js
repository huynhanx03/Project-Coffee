import { View, Text, ScrollView, TouchableOpacity } from "react-native";
import React, { useEffect, useState } from "react";
import { colors } from "../theme";

const Categories = (props) => {
    const [activeCategory, setActiveCategory] = useState("");
    const handleChangeCategory = (item) => {
        setActiveCategory(item);
    };
    return (
        <ScrollView horizontal showsHorizontalScrollIndicator={false} contentContainerStyle={{ paddingLeft: 15 }}>
            <View className="flex-row space-x-5">
                {props?.categories?.map((category, index) => {
                    const isActive = category === activeCategory;
                    return (
                        <TouchableOpacity
                            key={index}
                            onPress={() => handleChangeCategory(category)}
                            style={{ backgroundColor: isActive ? colors.active : "white" }}
                            className="p-2 rounded-xl">
                            <Text
                                className="text-lg font-semibold"
                                style={{ color: isActive ? "white" : colors.text(0.7) }}>
                                {category}
                            </Text>
                        </TouchableOpacity>
                    );
                })}
            </View>
        </ScrollView>
    );
};

export default Categories;
