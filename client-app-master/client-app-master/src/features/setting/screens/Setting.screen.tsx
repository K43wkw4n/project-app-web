import { View, Text, Dimensions, TouchableOpacity, Image } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { useAppDispatch } from "../../../store/store";
import { Logout } from "../../../store/context/accountSlice";

var width = Dimensions.get("window").width;

export const SettingScreen = () => {
  const dispatch = useAppDispatch();
 
  return (
    <SafeAreaView style={{ backgroundColor: "#fff", flex: 1 }}>
      <View style={{ alignItems: "center" }}>
        <Image
          style={{
            height: 220,
            width: width / 2 - 10,
          }}
          source={{
            uri: "https://static.vecteezy.com/system/resources/previews/008/442/086/original/illustration-of-human-icon-user-symbol-icon-modern-design-on-blank-background-free-vector.jpg",
          }}
        />
      </View>
      <TouchableOpacity
        onPress={() => {}}
        style={{
          padding: 20,
        }}
      >
        <Text
          style={{
            fontSize: 20,
          }}
        >
          Change Password
        </Text>
      </TouchableOpacity>
      <TouchableOpacity
        onPress={() => dispatch(Logout())}
        style={{
          padding: 20,
        }}
      >
        <Text
          style={{
            fontSize: 20,
          }}
        >
          Log out
        </Text>
      </TouchableOpacity>
    </SafeAreaView>
  );
};
