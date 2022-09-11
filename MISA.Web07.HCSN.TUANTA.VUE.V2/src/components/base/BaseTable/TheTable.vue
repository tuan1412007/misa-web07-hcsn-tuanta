<template>
    <div class="m-row assettable">
        <div class="table-notdata" v-if="isNoData">
            <div class="table-notdata__item">
                <div style = "padding-left: 60px" >Không tìm thấy dữ liệu.</div>
                <img style="width: 300px;height: 300px;" src="../../../assets/icon/nodatafound.png">
            </div>
        </div>
        <table class="table">
            <thead>
                <tr>
                    <th>
                        <button class="resize-width">
                            <label class="checkbox">
                                <input @click="isSelectAll()" type="checkbox" v-model="checked">
                                <span class="tick"></span>
                            </label>
                        </button>
                    </th>
                    <th class="text-align-center">
                        <button class="resize-width b tooltip">STT
                            <Tooltip tooltiptext="Số thứ tự" positiontooltip="top" style="margin-top:10px ;" />
                        </button> 
                    </th>
                    <th>
                        <button class="resize-width text-align-left">Mã tài sản</button>
                    </th>
                    <th>
                        <button class="resize-width text-align-left">Tên tài sản</button>
                    </th>
                    <th>
                        <button class="resize-width text-align-left">Loại sản phẩm </button>
                    </th>
                    <th>
                        <button class="resize-width text-align-left">Bộ phận sử dụng </button>
                    </th>
                    <th>
                        <button class="resize-width text-align-right ">Số lượng </button>
                    </th>
                    <th>
                        <button class="resize-width text-align-right ">Nguyên giá </button>
                    </th>
                    <th>
                        <button class=" text-align-right resize-width tooltip">HM/KH lũy kế
                            <Tooltip tooltiptext="Hao mòn/khấu hao lũy kế" positiontooltip="top" style="margin-top:10px" />
                        </button>
                    </th>
                    <th>
                        <button class="resize-width text-align-right ">Giá trị còn lại </button>
                    </th>
                    <th>
                        <button class="resize-width  text-align-center">Chức năng </button>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-contextmenu:contextmenu v-for="(item, index) in fixedAsset" :key="item.fixedAssetID"
                    @click="selectTick($event, item, index)" draggable="true" @dragstart="startDrag($event, item)"
                    :class="{ 'table-active': checkTick(item) }" @dblclick="isShowDialogDetail(item)">
                    <td >
                        <label class="checkbox">
                            <input @click="selectTick($event, item, index)" :checked="checkTick(item)" type="checkbox">
                            <span class="tick"></span>
                        </label>
                    </td>
                    <td class="text-align-center">{{ index + 1 }}</td>
                    <td class="text-align-left">{{ item.fixedAssetCode }}</td>
                    <td class="text-align-left"> <p style="width:200px; text-overflow:ellipsis; overflow:hidden; white-space:nowrap">{{ item.fixedAssetName }}</p> </td>
                    <td class="text-align-left">{{ nameFixedCategoryAsset(item.fixedAssetCategoryID) }}</td>
                    <td class="text-align-left">{{ nameDepartment(item.departmentID) }}</td>
                    <td class="text-align-right">{{ item.quantity }}</td>
                    <td class="text-align-right">{{ formatCash(item.cost) }}</td>
                    <td class="text-align-right">{{ (item.depreciationRate) }}</td>
                    <td class="text-align-right">{{ formatCash(item.depreciationYear) }}</td>
                    <td class="text-align-center">
                        <div class="table-control display-flex">
                            <div @click="isShowDialogDetail(item)" class="icon-mini icon-edit-mini icon__size-14 tooltip tooltipEventTable" style="margin-left:15px">
                                <Tooltip tooltiptext="Sửa" positiontooltip="top" style="top: 20px;" />
                            </div>
                            <div @click="isShowDialogDetail(item,true)" class="icon-mini icon-replication-mini icon__size-14 tooltip tooltipEventTable tooltipEventTableEdit" style="margin-left:10px">
                                <Tooltip tooltiptext="Nhân bản" positiontooltip="top"
                                    style="margin-left:-33px !important;top: 20px" />
                            </div>
                            <div class="icon-mini icon-remove-mini icon__size-14 tooltip tooltipEventTable tooltipEventTableRemove" @click="eventRemoveItem(item)" style="margin-left:10px">
                                <Tooltip tooltiptext="Xóa" positiontooltip="top"
                                    style="top: 20px;margin-left:-18px !important" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr style="height:0px;border:none"></tr>
                <tr class="table-paging">
                    <td class="text-align-left" colspan="6">
                        <div class="border-paging"></div>
                    </td>
                    <td class="text-align-right bold">
                        <div class="border-paging">{{ formatCash(sumQuantity) }}</div>
                    </td>
                    <td class="text-align-right bold">
                        <div class="border-paging">{{ computedSumCost }}</div>
                    </td>
                    <td class="text-align-right bold">
                        <div class="border-paging">{{ formatCash(sumDepreciation) }}</div>
                    </td>
                    <td class="text-align-right bold">
                        <div class="border-paging">{{ formatCash(sumResidualValue) }}</div>
                    </td>
                    <td>
                        <div class="border-paging" style=" width: 100%;"></div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="display-flex paging">
        <div class="display-flex paging_item" :class="{'setWidthPage':isSetWidth}">
            <p class="page__table--text">Tổng số : <span style="font-weight: bold;">{{ sumRecord }}</span>
                bản ghi</p>
            <Combobox ComboboxQuantity="true" style="width:100px" />
            <div class="table-paging__number display-flex">
                <div class="table-paging__number--item">
                    <div class="icon icon-back icon__size-8 tooltip" @click="backPage">
                        <Tooltip tooltiptext="Trang trước" positiontooltip="bottom" />
                    </div>
                </div>
                <div v-for="item in numberPageArray" :key="item" class="table-paging__number--item" @click="btnPageNumber(item)" :class="{'number-active' : item==checkNumber(item)}">{{ item }}</div>
                <div class="table-paging__number--item">
                    <div @click="nextPage" class="icon icon-next icon__size-8 tooltip">
                        <Tooltip tooltiptext="Trang sau" positiontooltip="bottom" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <v-contextmenu ref="contextmenu">
        <v-contextmenu-item>
            <div class="menu__item">
                <div class="menu__item--icon icon icon-add-menu icon__size-18"></div>
                <div class="menu__item--text">Thêm</div>
            </div>
        </v-contextmenu-item>
        <v-contextmenu-item>
            <div class="menu__item">
                <div class="menu__item--icon icon icon-edit icon__size-18" style="margin:0"></div>
                <div class="menu__item--text">Sửa</div>
            </div>
        </v-contextmenu-item>
        <v-contextmenu-item>
            <div class="menu__item">
                <div class="menu__item--icon icon icon-remove icon__size-18"></div>
                <div class="menu__item--text">Xóa</div>
            </div>
        </v-contextmenu-item>
        <v-contextmenu-item>
            <div class="menu__item">
                <div class="menu__item--icon icon icon-replication icon__size-18"></div>
                <div class="menu__item--text">Nhân bản</div>
            </div>
        </v-contextmenu-item>
    </v-contextmenu>
    <DialogX v-bind:dataError=titleWarning v-if="isShowDialogX" />
    <div class="loading" v-show="isLoading">
        <div class="loader"></div>
    </div>
</template>

<script>
import { formatCash } from '../../../js/common.js'
import { directive, Contextmenu, ContextmenuItem } from "v-contextmenu";
import "v-contextmenu/dist/themes/default.css";
import DialogX from '../BaseDialogX/TheDialogX.vue'
import Tooltip from '../BaseTooltip/TheTooltip.vue'
import axios from 'axios'
import Combobox from '../BaseCombobox/TheComboBox.vue'
export default {
    name: 'MISA.Web07.HCSN.TUANTA.VUE.v2',
    directives: {
        contextmenu: directive,
    },
    components: {
        [Contextmenu.name]: Contextmenu,
        [ContextmenuItem.name]: ContextmenuItem, Tooltip, Combobox,
        DialogX
    },
    data() {
        return {
            isLoading: false,
            directives: {
                contextmenu: directive,
                isNoData: false,
            },
            components: {
                [Contextmenu.name]: Contextmenu,
                [ContextmenuItem.name]: ContextmenuItem,
            },
            isClickRow: "",
            fixedAsset: [],
            sumQuantity: 0,
            sumCost: 0,
            sumDepreciation: 0,
            sumResidualValue: 0,
            isShowContentMenu: false,
            show: false,
            dataTicks: [],
            indexFirst: 0,
            indexLast: 0,
            sumRecord: 0,
            titleWarning: [],
            isShowDialogX: false,
            dataDepartment: [],
            dataCategory: [],
            searchArray: {},
            assetCode: "",
            checked: false,
            recordNumber: "10",
            numberPage: "10",
            numberPageArray: [],
            numbernext: 0,
            isCheckNumber:1,
            isSetWidth:false,
            tickedOld:false,
            replication:false
        }
    },
    computed: {
        computedSumCost() {
            return formatCash(this.sumCost)
        },
    },

    methods: {

        /**
         * Khai báo function format giá tiền
         * Author : TUANTA
         * Date : 09/08/2022
         */
        formatCash,

        /**
        * Bắn data của 1 row vào mảng
        * Author : TUANTA
        * Date: 10/08/2022
        */
        btnRowTable(item) {
            console.log(item);
        },

        /**
         * Show dialog nhân bản record
         * Author : TUANTA
         * Date : 10/08/2022
         */
        isShowDialogDetail(item,replication) {
            axios
            .get(`http://localhost:35535/api/v2/FixedAssets/GetOneRecord?id=${item.fixedAssetID}`)
            .then(res=>{
                console.log(res);
                if(replication == true){
                    this.emitter.emit("replication", item);
                    replication = false;
                }
                else
                {
                this.emitter.emit("itemDialog", res.data);
                this.emitter.emit("handlerEdit");
                }
            })
            .catch(error=>{
                console.log(error)
            })
        },

        /**
         * event nhấn giữ phím ctrl thì có thể chọn nhiều record
         * Author : TUANTA
         * Date:10/08/2022
         * @param(e,item,index) e là event được click vào
         *            item là truyền row vừa click trên table
         *            index là số thứ tự của row đó trong table 
         */
        selectTick(e, item, index) {
            try {
                //Nếu mã tài sản mới được thêm vào table thì focus vào row đó
                if (this.assetCode != "") {
                    for (let i = 0; i < this.fixedAsset.length; i++) {
                        if (this.assetCode == this.fixedAsset[i].fixedAssetCode) {
                            this.dataTicks.push(this.fixedAsset[i]);
                        }
                    }
                }
                
                //Nếu nhẫn phím Ctrl thì có thể thêm record vào mảng, mảng đó sẽ không bị reset
                //Nếu không nhẫn giữ phím Ctrl thì khi thêm record thì mảng đó sẽ bị reset
                if (e.ctrlKey == true) {
                    var dataTickList =[];
                    for(let i = 0 ; i<this.dataTicks.length;i++)
                    {
                        if(item == this.dataTicks[i])
                        {
                            this.dataTicks.splice(i, 1);
                            dataTickList = this.dataTicks;
                        }
                    }
                    if(dataTickList.length == 0)
                    {
                        this.dataTicks.push(item);
                    }
                    else{
                        this.dataTicks = dataTickList;
                    }
                }
                
                /**
                * Nếu nhẫn giữ phím Shift thì có thể chọn được đến vị trí record khi nháy chuột tiếp theo
                * Author : TUANTA
                * Date: 10/08/2022
                * */
                else if (e.shiftKey == true) {
                    this.indexLast = index;
                    var intermediary = "";
                    //Nếu vị trí indexLast ở bên trên indexFirst thì đổi ngược lại 2 vị trí cho nhau
                    //Tức là indexLast sẽ trở thành indexFirst và ngược lại
                    if (this.indexLast < this.indexFirst) {
                        intermediary = this.indexLast;
                        this.indexLast = this.indexFirst;
                        this.indexFirst = intermediary;
                    }
                    // Gán số lượng được chọn về rỗng
                    this.dataTicks = [];
                    for (var i = this.indexFirst; i <= this.indexLast; i++) {
                        this.dataTicks.push(this.fixedAsset[i]);
                    }
                }
                else {
                    // Nếu click vào 1 record đã được chọn trước đó thì huỷ tick
                    if (this.dataTicks[0] == item) {
                        this.dataTicks = [];
                    }
                    // Nếu click vào 1 record không phải record trước đó thì huỷ tick ở reocrd trước đó
                    else {
                        this.dataTicks = [];
                        this.dataTicks.push(item);
                        this.indexFirst = index;
                    }
                }
                // Kiểm tra nếu số lượng tick hết = số lượng bản ghi đang hiển thị thì tick all
                //Nếu tích chọn hết các record thì trở thành thành tích chọn tất cả
                if (this.dataTicks.length == this.fixedAsset.length) {
                    this.checked = true;
                }
                else {
                    this.checked = false;
                }
                this.emitter.emit("titleWarning", this.dataTicks);
            } catch (error) {
                console.log(error);
            }
        },

        /**
        * Kiểm tra những row đã được tích
        * Author : TUANTA
        * Date: 10/08/2022
        */
        checkTick(item) {
            try {
                for (const value of this.dataTicks) {
                    if (item == value) {
                        return item;
                    }
                }
            } 
            catch (error) {
                console.log(error);
            }
        },

        /**
        * Chọn tất cả record
        * Author : TUANTA
        * Date: 10/08/2022
        */
        isSelectAll() {
            this.checked = !this.checked;
            if (this.checked == true) {
                for (const item of this.fixedAsset) {
                    this.dataTicks.push(item);
                }
            }
            else {
                this.dataTicks = [];
                return
            }
        },

        /**
         * event xoá record
         * Author : TUANTA
         * Date : 10/08/2022
         */

        eventRemoveItem(item) {
            this.isShowDialogX = true;
            this.titleWarning.push(`Bạn có muốn xóa tài sản ${item.fixedAssetName} này không ?`);
        },

        /**
        * Get tên bộ phận sử dụng
        * Author :TUANTA
        * Date : 14/08/2022
        */
        nameDepartment(id) {
            for (const item of this.dataDepartment) {
                if (item.DepartmentID == id) {
                    return item.DepartmentName;
                }
            }
        },

        /**
         * Lấy tên loại tài sản
         * Author : TUANTA
         * Date:14/08/2022
         */
        nameFixedCategoryAsset(id) {
            for (const item of this.dataCategory) {
                if (item.FixedAssetCategoryID == id) {
                    return item.FixedAssetCategoryName;
                }
            }
        },

        /**
        * Tính số lượng trang
        * Author : TUANTA
        * Date: 18/08/2022
        */
        numberPages(sumRecord) {
            if (sumRecord != undefined) {
                this.numberPage = sumRecord / this.recordNumber;
            }
        },

        /**
        * Next trang
        * Author : TUANTA
        * Date: 18/08/2022
        */
        nextPage() {
            // sử dụng để tính toán biến i trong vòng for
            let number = 1;
            if (this.numbernext <= this.numberPage) {
                // Khi người dùng nhấn next trang sẽ lấy thêm 2 số tiếp theo
                if (this.numbernext >= 2) {
                    number = this.numbernext;
                }
                // default i = 1
                else {
                    number = 1;
                }
                // Lấy 2 số tiếp theo 
                this.numbernext = this.numbernext + 2;
                this.numberPageArray = [];

                // lấy 2 số đầu gần nhất
                for (let i = number; i <= this.numberPage; i++) {
                    if (i < this.numbernext) {
                        this.numberPageArray.push(i);
                    }
                }
                //  Những số ở giữa mà không được hiển thị sẽ thay bằng dấu 3 chấm ...
                this.numberPageArray.push("...");
                // lấy 2 số cuối liên tiếp
                for (let i = 1; i <= this.numberPage; i++) {
                    if (i > this.numberPage - 2) {
                        this.numberPageArray.push(i);
                    }
                }
            }
            else {
                this.numbernext = 0
            }
        },

        /**
         * Back trang
         * Author : TUANTA
         * Date : 17/08/2022
         */
        backPage() {
            let number = 1;
            if(this.numbernext!=0)  
            {
                number = this.numbernext - 4;  
                this.numberPageArray = [];
                this.numbernext=this.numbernext-2;
                // lấy 2 số đầu gần nhất
                for (let i = number; i <=this.numberPage; i++) { 
                    if (i < this.numbernext) {  
                        this.numberPageArray.push(i);
                    }
                }
                //  Những số ở giữa mà không được hiển thị sẽ thay bằng dấu 3 chấm ...
                this.numberPageArray.push("...");
                // lấy 2 số sau gần nhất
                for (let i = 1; i <= this.numberPage; i++) {
                    if (i > this.numberPage - 2) {
                        this.numberPageArray.push(i);
                    }
                }
            }
        },

        /**
         * Chọn số trang muốn hiển thị
         * Author : TUANTA
         * Date : 17/08/2022
         */
        btnPageNumber(item){
            this.isCheckNumber = item;
            // truyền số trang muốn hiển thị
            this.emitter.emit("pageNumber",item);
        },

        /*
         * Get data vào bảng
         * Author : TUANTA
         * Date : 14/08/2022
         */
        getRecord() {
            var httpSearch = `http://localhost:35535/api/v2/FixedAssets/filter?`;
            if (this.searchArray.keyword != undefined) {
                if (this.searchArray.keyword != "") {
                    httpSearch = httpSearch + `keyword=${this.searchArray.keyword}&`;
                }
                if (this.searchArray.fixedAssetCategoryID != "") {
                    httpSearch = httpSearch + `fixedAssetCategoryID=${this.searchArray.fixedAssetCategoryID}&`;
                }
                if (this.searchArray.departmentID != "") {
                    httpSearch = httpSearch + `departmentID=${this.searchArray.departmentID}&`;
                }
                if (this.searchArray.pageSize != "") {
                    httpSearch = httpSearch + `pageSize=${this.searchArray.pageSize}&`;
                }
                if(this.searchArray.pageNumber != "")
                {
                    httpSearch = httpSearch + `pageNumber=${this.searchArray.pageNumber}`;
                }

            }
            this.isLoading = true;
            axios
            .get(httpSearch)
            .then(response => {
                this.fixedAsset = response.data.data;
                console.log(response);
                this.sumRecord = response.data.totalCount;
                this.numberPages(this.sumRecord);
                this.selectTick();
                this.isLoading = false;
                if (response.data.data.length == 0) {
                    this.isNoData = true;
                }
                else {
                    this.isNoData = false;
                }

                /*
                *tính tổng các trường trong table
                * Author : TUANTA
                * Date : 09/08/2022
                */
                // set lại các biến tổng về 0
                this.sumQuantity = 0;
                this.sumCost = 0;
                this.sumDepreciation = 0;
                this.sumResidualValue = 0;
                // sử dụng vòng lặp để tính tổng 
                for (const item of this.fixedAsset) {
                    this.sumQuantity = (this.sumQuantity + item.quantity);
                    this.sumCost = (this.sumCost + item.cost);
                    this.sumDepreciation = 0;
                    this.sumResidualValue = (this.sumResidualValue + item.trackedYear);
                }
            })
            .catch(error => {
                this.isNoData = true;
                alert(error);
            })
        },

        checkNumber(item){
            if(item == this.isCheckNumber)
            {
                return item;
            }
        },

        startDrag(evt, item) {
            evt.dataTransfer.dropEffect = 'move'
            evt.dataTransfer.effectAllowed = 'move'
            evt.dataTransfer.setData('itemID', item)
        },
        onDrop(evt, list) {
            const itemID = evt.dataTransfer.getData('itemID')
            const item = this.items.find((item) => item.id == itemID)
            item.list = list
        },

    },

    created() {
        /*
        * Gọi Hàm lấy dữ liệu đưa vào table
        * Author : TUANTA
        * Date : 14/08/2022
        */
        this.getRecord();

        // Lấy thông tin bộ phận sử dụng từ API
        try {

            axios
            .get("http://localhost:35535/api/v2/Departments")
            .then(res => {
                console.log(res);
                this.dataDepartment = res.data;
            })
            .catch(res => {
                console.log(res);
            })
        } 
        catch (error) {
            console.log(error);
        }

        // lấy thông tin loại tài sản từ API
        try {
            axios
            .get("http://localhost:35535/api/v2/FixedAssetCategories")
            .then(res => {
                console.log(res);
                this.dataCategory = res.data;
            })
            .catch(res => {
                console.log(res);
            })
        } 
        catch (error) {
            console.log(error);
        }
        this.nextPage(this.numbernext);
    },

    mounted() {
        // Nhận lời gọi loading lại data từ dialog
        this.emitter.on("LoadData", (assetCode) => {
            // Gọi hàm load data
            this.titleWarning = [];
            this.dataTicks=[];
            this.assetCode = "";
            this.assetCode = assetCode;
            this.getRecord();
        }),

        this.emitter.on("search", (searchArray) => {
            this.searchArray = searchArray;
            // Gọi hàm load data
            this.getRecord();
        }),

        // Nhận số lượng bản ghi hiển thị
        this.emitter.on("dataPageSize", item => {
            this.recordNumber = item;
            this.numberPages(this.sumRecord);
            this.numbernext=0;
            this.nextPage(this.numbernext);
        }),

        // Set lại width Navbar
        this.emitter.on("setWidth", () => {
            // Set width cho Container
            this.isSetWidth = true
        }),

        // Reset width Navbar
        this.emitter.on("setWidthClear", () => {
            // Reset width Container
            this.isSetWidth = false
        })
    }
};
</script>