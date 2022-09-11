<template>
    <div class="wrapper" :class="{'setWidth':isSetWidth}">
        <Header />
        <div class="wrapper-content">
            <div class="m-row m-row__form" style="">
                <div class="m-row__left display-flex">
                    <!-- tạo input search -->
                    <div class="input-search" style="margin-right:10px" >
                        <div class="icon input__search--icon__item input__search--icon" data-toggle="tìm kiếm"></div>
                        <input type="text" name="" id="" class="input input__search--text" @keydown.enter="eventSearch" placeholder="Tìm kiếm tài sản">
                    </div>
                    <ComboBox val="Loại tài sản" v-bind:option=optionFixedAssetCategory icon="true"  nameTable="FixedAssetCategory" @dataComBoBoxSearch="eventSearch" @remove="eventSearch" />
                    <ComboBox val="Bộ phận sử dụng" v-bind:option=optionDepartment icon="true" nameTable="Department" @dataComBoBoxSearch="eventSearch" @remove="eventSearch" />
                </div>

                <div class="m-row__right display-flex">
                    <div @click="btnShowDialog()" class="btn btn__icon" id="btn--add">
                        <div class="btn__icon--item">
                            <div class="btn--text">Thêm tài sản</div>
                        </div>
                    </div>
                    <button class="btn-control distance tooltip" @click="exportExcel">
                        <div class="btn-control__more icon icon-more icon__size-18"></div>
                         <Tooltip tooltiptext="Xuất Excel" positiontooltip="bottom"/>
                    </button>
                    <button class="btn-control tooltip" @click="eventRemove()">
                        <div class="btn-control__remove icon icon-remove icon__size-18"></div>
                         <Tooltip tooltiptext="Xóa" positiontooltip="bottom"/>
                    </button>
                </div>
            </div>
            <router-view></router-view>
        </div>
    </div>
    <TheDialog v-if="isShowDialog" @btnCloseDialog="isShowDialog = false" :title="title"
    v-bind:item="itemFixedAssetDetail" :handler=handler @handlerName="showSuccess" />
    <DialogX v-if="isShowDialogX" :dataError=titleWarning @isShowDialogX ="isShowDialogXControl()" @removeData="isRemove =true" :dataAsset=dataTicks :buttonNames="['Xóa','Không']" />  
    <div class="toast__box" v-show="isShowSuccess == true">
        <div class="toast__box--item">
            <div class="toast__box--body"><div class ="toast__box--item-icon"><div class="icon icon-tick-white icon__size-11x2"></div></div></div>
            <div class="toast__box--item-text">{{handlerName}} dữ liệu thành công</div>
        </div>
     </div>
</template>
<script>
import Header from './TheHeader.vue'
import ComboBox from '../base/TheComboBox.vue'
import TheDialog from '../base/TheDialog.vue'
import Tooltip from '../base/TheTooltip.vue'
import DialogX from '../base/TheDialogX.vue'
import axios from 'axios'
export default {
    name: "TheHeader",
    components: {
        Header, ComboBox, TheDialog,Tooltip,DialogX
    },
    data() {
        return {
            isShowDialogX : false,
            isShowDialog: false,
            title:"Sửa tài sản",
            titleWarning:[],
            dataTicks:[],
            optionDepartment: [],
            optionFixedAssetCategory: [],
            itemFixedAssetDetail :[],
            isSetWidth:false,
            handler:"",
            isShowSuccess:false,
            searchArray :{
                keyword : "",
                fixedAssetCategoryID : "",
                departmentID : "",
                pageSize:"",
                pageNumber:"",
                handlerName:""
            },
            dataReplication:[],
            isRemove :false,
        };
    },
    methods: {
        /**
         * Nhấn nút thêm mới thì hiển thị dialog thêm mới tài sản
         * Author : TUANTA
         * Date :09/08/2022
         */
        btnShowDialog(){
            try {
                // set lại input về trạng thái rỗng
                this.itemFixedAssetDetail=[]
                this.handler="add";
                // get mã tài sản mới nhất thông qua API
                axios
                .get('http://localhost:35535/api/v2/FixedAssets/NewCodeFixedAsset')
                .then(res=>{
                    this.isShowDialog = true ;
                    this.title="Thêm tài sản";
                    console.log(res);
                    // Nếu là chức năng nhân bản thì truyền dữ liệu của record đó vào form
                    if(this.dataReplication != undefined)
                    {
                        this.itemFixedAssetDetail = this.dataReplication;
                        this.dataReplication = [];
                    }
                    this.itemFixedAssetDetail.fixedAssetCode = res.data;
                })
                .catch(res=>{
                    console.log(res);
                })
            } catch (error) {
                console.log(error);
            }
        },

         /**
         * event xoá data
         * Author : TUANTA
         * Date:10/08/2022
         */
        eventRemove(){
            try {
                this.titleWarning = [];
                this.isShowDialogX = true;
                var quantity = this.dataTicks.length;
                if(quantity == 0){
                    this.titleWarning.push("Bạn cần chọn tài sản trước khi xóa.");
                }
                else if(quantity==1){
                    this.titleWarning.push(`Bạn có thật sự muốn xóa tài sản ${this.dataTicks[0].fixedAssetName} ?`);
                }
                else{
                    this.titleWarning.push(`Bạn có thật sự muốn xóa ${quantity} tài sản ?`);
                }
            } 
            catch (error) {
                console.log(error);
            }
        },

        /**
         * Nếu xoá thành công thì ẩn dialog
         * Author : TUANTA
         * Date:10/08/2022
         */
        isShowDialogXControl(){
            this.isShowDialogX = false;
            if(this.isRemove)
            {
                this.dataTicks = [];
                this.isRemove = false;
            }

        },

        /**
         * Nhận dữ liệu khi lọc bằng keyword, fixedAssetCategoryID, departmentID
         * Author : TUANTA
         * Date:10/08/2022
         */
        eventSearch(item){
            if(item.DepartmentID=="")
            {
                this.searchArray.departmentID = "";
            }
            if(item.FixedAssetCategoryID =="")
            {
                this.searchArray.fixedAssetCategoryID = "";
            }
            if(item.DepartmentID != undefined)
            {   
                this.searchArray.departmentID= item.DepartmentID;
            }
            if(item.FixedAssetCategoryID != undefined)
            {   
                 this.searchArray.fixedAssetCategoryID= item.FixedAssetCategoryID;
            }
            if(event.currentTarget.value != undefined)
            {
                this.searchArray.keyword= event.currentTarget.value;
            }
            if(item.pageSize != undefined)
            {
                this.searchArray.pageSize = item;
            }
            if(item.pageNumber!=undefined)
            {
                this.searchArray.pageNumber = item;
            }
            this.emitter.emit("search",this.searchArray);
        },

        /**
         * Hiển thị dialogX thông báo rằng thêm hoặc sửa thành công
         * Author : TUANTA
         * Date :14/08/2022
         */
        showSuccess(item){
            if(item == "Sửa")
            {
                this.handlerName="Sửa"
            }
            else{
                 this.handlerName="Thêm"
            }
            this.isShowSuccess = true;
            setTimeout(()=>{ this.isShowSuccess = false },2000);
        },
    },

    /**
     * Nhận data từ combobox
     * Author : TUANTA
     * Date :14 /08/ 2022
     */
    created(){
        // Getdata Department thông qua API
        try {
            axios
            .get("http://localhost:35535/api/v2/Departments")
            .then(res=>{
                console.log(res);
                this.optionDepartment = res.data;
                this.emitter.emit("Department",res.data);
            })
            .catch(res=>{
                console.log(res);
            })
        } 
        catch (error) {
            console.log(error);
        }

        // getdata fixedAssetCategory thông qua API
        try {
            axios
            .get("http://localhost:35535/api/v2/FixedAssetCategories")
            .then(res=>{
                console.log(res);
                this.optionFixedAssetCategory = res.data;
                // bắn data vào bảng dữ liệu
                this.emitter.emit("Category",res.data);
            })
            .catch(res=>{
                console.log(res);
            })
        } 
        catch (error) {
            console.log(error);
        }
    },

    mounted(){
        /**
         * Nhận data từ Eventbus
         * Author : TUANTA
         * Date :09/08/2022
         */
        try {
            // Nhận data từ bảng dữ liệu
            this.emitter.on("itemDialog", (item) => {
                // Mở dialog
                this.isShowDialog = true,
                this.title="Sửa tài sản";
                // Truyền data của record vào mảng item
                this.itemFixedAssetDetail = item
                this.handler = "edit";
            }),

            // Set lại width cho navbar
            this.emitter.on("setWidth", () => {
                // set width cho content
                this.isSetWidth = true
            }),
            
            // Reset width cho navbar
            this.emitter.on("setWidthClear", () => {
                // set width cho content
                this.isSetWidth = false
            }),

            //
            this.emitter.on("titleWarning", (item) => {
                // bắn data vào cho mảng item
                this.dataTicks = item
            }),

            //
            this.emitter.on("dataPageSize",(item)=>{
                // bắn data vào cho mảng item
                this.searchArray.pageSize = item
                this.eventSearch(item)
            }),

            //
            this.emitter.on("pageNumber",item=>{
                // bắn data vào cho mảng item
                this.searchArray.pageNumber = item
                this.eventSearch(item)
            })

            //
            this.emitter.on("replication",item=>{
                // Nhận dữ liệu cần nhân bản
                this.dataReplication =  item;
                this.btnShowDialog();
            })
        } 
        catch (error) {
            console.log(error);
        }    
    },
}
</script>
<style>
@import url(../../css/main.css);
</style>