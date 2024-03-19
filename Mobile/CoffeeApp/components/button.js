import { View, Text, TouchableOpacity } from "react-native";
import React from "react";
import { colors } from "../theme";

export default function Button({content, handle}) {
    return (
        <TouchableOpacity className="p-5 rounded-lg" style={{ backgroundColor: colors.primary }}>
            <Text className="text-white text-center font-semibold text-base">{content}</Text>
        </TouchableOpacity>
    );
}
