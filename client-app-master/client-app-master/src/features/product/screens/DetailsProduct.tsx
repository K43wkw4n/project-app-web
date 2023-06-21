import { View, Text, TouchableOpacity, Image, Dimensions } from "react-native";
import React, { useEffect, useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import Loading from "../../../screens/Loading";
import { useAppDispatch, useAppSelector } from "../../../store/store";
import { AntDesign } from "@expo/vector-icons";

var width = Dimensions.get("window").width;

export const DetailsProduct = ({ route, navigation }: any) => {
  const [load, setLoad] = useState(true);
  const { data }: any = route.params;
  const { item } = data;

  useEffect(() => {
    setTimeout(() => {
      setLoad(false);
    }, 1000);
  }, []);

  console.log("card", data);

  return (
    <>
      <SafeAreaView style={{ backgroundColor: "#fff", flex: 1 }}>
        <TouchableOpacity
          style={{
            position: "absolute",
            zIndex: 1,
            marginTop: 20,
          }}
          onPress={() => navigation.goBack()}
        >
          <AntDesign
            style={{
              padding: 20,
            }}
            name="back"
            size={24}
            color="black"
          />
        </TouchableOpacity>
        <View>
          <Image
            style={{
              height: 250,
              width: width,
              borderBottomRightRadius: 40,
              borderBottomLeftRadius: 40,
            }}
            source={{
              uri: item.image,
            }}
          />
        </View>
        <View>
          <View>
            <Text
              style={{
                marginLeft: 20,
                marginTop: 20,
                fontWeight: "bold",
                fontSize: 20,
              }}
            >
              {item.name}
            </Text>
            <Text style={{ marginLeft: 20 }}>{item.description}</Text>
          </View>
        </View>
      </SafeAreaView>
      {load && <Loading />}
    </>
  );
};
