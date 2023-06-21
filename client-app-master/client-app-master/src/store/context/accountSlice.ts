import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit";
import { User } from "../../models/User";
import { FieldValues } from "react-hook-form";
import agent from "./api/agent";
import { HttpStatusCode } from "axios";
import { Alert } from "react-native";
import AsyncStorage from "@react-native-async-storage/async-storage";

interface account {
  token: any;
}

const initialState: account = {
  token: "",
};

export const signInUser = createAsyncThunk<User, FieldValues>(
  "account/LoginUser",
  async (data, thunkAPI) => {
    try {
      const userDto = await agent.Account.login({
        userName: data.userName,
        password: data.password,
      });
      const { ...user } = userDto;
      return user;
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data });
    }
  }
);

const storeSaveToken = async (value: any) => {
  try {
    const jsonValue = JSON.stringify(value);
    await AsyncStorage.setItem("@Token", jsonValue);
  } catch (e) {
    // saving error
  }
};

const storeRemoveToken = async () => {
  try {
    await AsyncStorage.clear();
  } catch (exception) {
    console.log(exception);
  }
};

export const AccountSlice = createSlice({
  name: "AccountSlice",
  initialState: initialState,
  reducers: {
    Logout: (state) => {
      state.token = "";
      storeRemoveToken();
    },
    AlreadyLogin: (state, action) => {
      state.token = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      .addMatcher(isAnyOf(signInUser.fulfilled), (state, action: any) => {
        console.log("getUser", action.payload.userDto.token);
        if (action.payload.statusCode.statusCode == HttpStatusCode.Ok) {
          state.token = action.payload;
          //console.log("getTokenUser", action.payload.userDto.token);
          storeSaveToken(action.payload);
        } else if (state.token.statusCode == HttpStatusCode.NotFound) {
          Alert.alert("NotFound", "Please check the information you entered.", [
            { text: "OK" },
          ]);
        } else {
          Alert.alert("Input not null", "Please enter some information.", [
            { text: "OK" },
          ]);
        }
      })
      .addMatcher(isAnyOf(signInUser.rejected), (state, action: any) => {
        state.token = "";
        console.log("signInUser is rejected");
      });
  },
});

export const { Logout, AlreadyLogin } = AccountSlice.actions;
