<template>
    <div class="dialog__handle" id="dialog__notify">
        <div class="dialog__notify">
            <div class="dialog__notify--header">
                <div class="icon-warning"><i class="fa-solid fa-triangle-exclamation"></i></div>
                <div  class ="text--notify">
                    <p v-for="item in dataError" :key="item">{{item}} <b>{{errorName}}</b></p>
                </div>
            </div>
            <div class="dialog__notify--footer">
                <button class="btn btn-confirm" ref="btnConfirm" tabindex="1" @click="eventConfirm()" >
                    {{buttonNames[0]}}
                </button>
                <button v-if="buttonNames[1] != undefined " class="btn btn-cancel" ref="btnCancel" style="margin:0 10px;" @keydown.tab.prevent="isFocusConfirm()" tabindex="2" @click="removeDialog()">
                   {{buttonNames[1]}}
                </button>
                  <button v-if="buttonNames[2] != undefined " class="btn btn-cancel" @keydown.tab.prevent="isFocusConfirm()" tabindex="3" @click="this.$emit('isShowDialogX')">
                    {{buttonNames[2]}}
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    name: 'TheDialogNotify',

    data() {
        return {
            ShowDialogNotify:true,
        };
    },

    mounted() {
        this.$refs['btnConfirm'].focus();

    },
    props:{
        dataError:Array,
        errorName:String,
        dataAsset:Array,
        buttonNames:Array,
        handleButton:String
    },

    methods: {
        /***
         * Mặc định sẽ Focus vào ô xác nhận
         * Author: TUANTA
         * Date:13/08/2022
         */
        isFocusConfirm(){
            
        this.$refs['btnConfirm'].focus();
        },

        /**
         * event xoá một record , xoá nhiều record
         * Author:TUANTA
         * Date:15/08/2022
         */
        eventConfirm(){
            try {
                if(this.buttonNames.length==1)
                {
                    this.$emit("isShowDialogX");
                }
                // Kiểm tra chức năng là lưu hay thêm mới
                if(this.buttonNames[2] != undefined)
                {
                    this.$emit("isSaveData");
                }
                else{
                    // Xóa 1 record
                    if(this.dataAsset.length == 1)
                    {
                        axios
                        .post(`http://localhost:35535/api/v2/FixedAssets/delete?id=${this.dataAsset[0].fixedAssetID}`)
                        .then(res=>{
                            console.log(res);
                            this.$emit("removeData");
                            this.$emit("isShowDialogX");
                            this.emitter.emit("LoadData");
                        })
                        .catch(res=>{
                            console.log(res);
                        })
                    }
                    
                    // Xóa nhiều record
                    if(this.dataAsset.length>1){
                        var idAssets = [];
                        for(let i =0 ;i<this.dataAsset.length ; i++)
                        {
                            idAssets.push(this.dataAsset[i].fixedAssetID);
                        }
                        axios
                        .post(`http://localhost:35535/api/v2/FixedAssets/deleteFixedAssets`,idAssets)
                        .then(res=>{
                            console.log(res);
                            this.$emit("removeData");
                            this.$emit("isShowDialogX");
                            this.emitter.emit("LoadData");
                        })
                        .catch(res=>{
                            console.log(res);
                        })
                    }
                }
            } 
            catch (error) {
                console.log(error);
            }
        },

        /**
         * Đóng dialogX
         * Author: TUANTA
         * Date:15/08/2022
         */
        removeDialog(){
            try {
                if(this.buttonNames[2] != undefined)
                {
                    this.$emit("removeDialog");
                }
                else{
                    this.$emit("isShowDialogX");
                }  
            }
            catch (error) {
                console.log(error);
            }
        }
    },    
};
</script>

<style lang="css" scoped>

</style>