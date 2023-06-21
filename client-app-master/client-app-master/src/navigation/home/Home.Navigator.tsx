import { View, Text } from "react-native";
import React from "react";
import { createStackNavigator } from "@react-navigation/stack";
import { Loginscreen } from "../../features/account/screens/Login.screen";
import { RegisterScreen } from "../../features/account/screens/Register.screen";
import { AccountScreen } from "../../features/account/screens/Account.screen";
import { HomeScreen } from "../../features/home/screens/Home.screen";
import { DetailsProduct } from "../../features/product/screens/DetailsProduct";

const Stack = createStackNavigator();

export const HomeNavigator = () => {
  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="Home1" component={HomeScreen} />
      <Stack.Screen name="DetailsProduct" component={DetailsProduct} /> 
      {/* <Stack.Screen
        name="Profile"
        component={Profile}
        initialParams={{ userId: user.id }}
      />
      <Stack.Screen name="Feed" component={Feed} /> */}
    </Stack.Navigator>
  );
};
