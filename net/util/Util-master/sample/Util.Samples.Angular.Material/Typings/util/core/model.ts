//============== 模型=========================
//Copyright 2018 何镇汐
//Licensed under the MIT license
//================================================

/**
 * 标识
 */
export interface IKey {
    /**
     * 标识
     */
    id: string;
}

/**
 * 视图模型
 */
export class ViewModel implements IKey {
    /**
     * 标识
     */
    id: string;
}

/**
 * 查询参数
 */
export class QueryParameter {
    /**
     * 页索引，即第几页
     */
    page: number;
    /**
     * 每页显示行数
     */
    pageSize: number;
    /**
     * 排序条件
     */
    order: string;
    /**
     * 搜索关键字
     */
    keyword: string;
}